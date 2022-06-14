using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Snowflake;
using Snowflake.Windows;

namespace Sandbox;

internal class SandboxBootstrapper : Bootstrapper<SandboxApp>
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);
        services.AddSingleton<Func<IWindow>>(() => new WindowsWindow(new WindowProps()));
        services.AddSingleton<ExampleLayer>();
    }

    protected override SandboxApp CreateApplication(IServiceProvider serviceProvider) 
        => new SandboxApp(
            serviceProvider.GetRequiredService<ILogger<Application>>(), 
            serviceProvider.GetRequiredService<Func<IWindow>>(),
            serviceProvider.GetRequiredService<ExampleLayer>());
}
