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
        m_logger.LogInformation("Information from snowflake!");
        m_logger.LogWarning("Warning from snowflake!");
        m_logger.LogError("Error from snowflake!");
        while (true);
    }
}
