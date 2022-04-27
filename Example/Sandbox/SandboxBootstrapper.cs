using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Snowflake;

namespace Sandbox;

internal class SandboxBootstrapper : Bootstrapper<SandboxApp>
{
    protected override SandboxApp CreateApplication(IServiceProvider serviceProvider) 
        => new SandboxApp(serviceProvider.GetRequiredService<ILogger<Application>>());
}
