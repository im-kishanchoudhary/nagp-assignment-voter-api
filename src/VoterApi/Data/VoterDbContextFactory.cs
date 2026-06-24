using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VoterApi.Data;

// Used only by the EF Core tooling (migrations add / bundle) at design time.
// EF does not open a connection here. The real one is passed at deploy time
// via `efbundle --connection`. The string below is a design-time placeholder,
// overridable by an env var, and never points at a live database.
public class VoterDbContextFactory : IDesignTimeDbContextFactory<VoterDbContext>
{
    public VoterDbContext CreateDbContext(string[] args)
    {
        var designTimeConnection =
            Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
            ?? "Host=design-time;Database=voterdb;Username=design;Password=design";

        var options = new DbContextOptionsBuilder<VoterDbContext>()
            .UseNpgsql(designTimeConnection)
            .Options;

        return new VoterDbContext(options);
    }
}
