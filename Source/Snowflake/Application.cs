using Microsoft.Extensions.Logging;

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
        while (true);
    }
}
