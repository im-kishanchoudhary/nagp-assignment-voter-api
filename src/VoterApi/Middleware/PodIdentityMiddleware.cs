namespace VoterApi.Middleware;

// Stamps every response with the pod and node that handled the request.
// Values come from the Kubernetes downward API (env vars on the pod).
public class PodIdentityMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _podName;
    private readonly string _nodeName;

    public PodIdentityMiddleware(RequestDelegate next)
    {
        _next = next;
        _podName = Environment.GetEnvironmentVariable("POD_NAME") ?? Environment.MachineName;
        _nodeName = Environment.GetEnvironmentVariable("NODE_NAME") ?? "unknown";
    }

    public Task InvokeAsync(HttpContext context)
    {
        context.Response.OnStarting(() =>
        {
            context.Response.Headers["X-Pod-Name"] = _podName;
            context.Response.Headers["X-Node-Name"] = _nodeName;
            context.Response.Headers["X-Version"] = "V2";
            return Task.CompletedTask;
        });

        return _next(context);
    }
}
