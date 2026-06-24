using Microsoft.EntityFrameworkCore;
using VoterApi.Data;
using VoterApi.Middleware;
using VoterApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build the Postgres connection string from environment values so nothing
// sensitive or environment-specific is baked into the image.
var connectionString = BuildConnectionString(builder.Configuration);

builder.Services.AddDbContext<VoterDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IVoterRepository, VoterRepository>();
builder.Services.AddScoped<IVoterService, VoterService>();

builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("database", tags: new[] { "ready" });

var app = builder.Build();

app.UseMiddleware<PodIdentityMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

// Liveness: process is up. Stays independent of the database on purpose.
app.MapHealthChecks("/health/live", new()
{
    Predicate = _ => false
});

// Readiness: only passes when the database is reachable.
app.MapHealthChecks("/health/ready", new()
{
    Predicate = check => check.Tags.Contains("ready")
});

// Root landing endpoint so hitting the IP/host directly shows the API is up.
app.MapGet("/", () => Results.Ok(new { status = "Voter API is running" }));

app.MapControllers();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Voter API starting on pod {Pod}, node {Node}",
    Environment.GetEnvironmentVariable("POD_NAME") ?? Environment.MachineName,
    Environment.GetEnvironmentVariable("NODE_NAME") ?? "local");

app.Run();

static string BuildConnectionString(IConfiguration config)
{
    var host = config["DB_HOST"] ?? "localhost";
    var port = config["DB_PORT"] ?? "5432";
    var database = config["DB_NAME"] ?? "voterdb";
    var user = config["DB_USER"] ?? "voter";
    var password = config["DB_PASSWORD"] ?? "voter";

    return $"Host={host};Port={port};Database={database};Username={user};Password={password};Pooling=true";
}
