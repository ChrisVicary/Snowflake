using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Snowflake;

public abstract class Bootstrapper<T> where T : Application
{
    public virtual void Run()
    {
        IServiceCollection services = new ServiceCollection();
        ConfigureServices(services);
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        StartApplication(serviceProvider.GetRequiredService<T>());
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
        services.AddLogging(builder => 
            builder.AddSimpleConsole(options =>
            {
                options.SingleLine = true;
                options.TimestampFormat = "hh:mm:ss - ";
            })
            .SetMinimumLevel(LogLevel.Trace))
            .AddSingleton(CreateApplication);
    }

    protected abstract T CreateApplication(IServiceProvider serviceProvider);

    protected virtual void StartApplication(T application) => application.Run();
}