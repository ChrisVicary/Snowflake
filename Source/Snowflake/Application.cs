using Microsoft.Extensions.Logging;
using Snowflake.Events;

namespace Snowflake;

public abstract class Application
{
    private readonly ILogger<Application> m_logger;

    public Application(ILogger<Application> logger)
    {
        m_logger = logger;
    }

    public virtual void Run()
    {
        m_logger.LogInformation("Snowflake Initialized.");

        var windowResize = new WindowResizeEvent(800, 600);
        m_logger.LogTrace(windowResize.ToString());

        while (true);
    }
}
