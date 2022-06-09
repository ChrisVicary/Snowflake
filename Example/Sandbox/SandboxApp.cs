using Microsoft.Extensions.Logging;
using Snowflake;

namespace Sandbox;

internal class SandboxApp : Application
{
    public SandboxApp(ILogger<Application> logger, Func<IWindow> windowFactory)
        : base(logger, windowFactory) { }
}
