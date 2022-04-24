using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Snowflake.Benchmark.Logging;

[MemoryDiagnoser]
public class LoggingBenchmarks
{
    private const string LogMessageWithParameters = "This is a log message with parameters {first} and {second}.";

    private readonly ILoggerFactory m_loggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddFakeLogger().SetMinimumLevel(LogLevel.Information);
    });

    private readonly ILogger<LoggingBenchmarks> m_logger;

    public LoggingBenchmarks()
    {
        m_logger = new Logger<LoggingBenchmarks>(m_loggerFactory);
    }

    [Benchmark]
    public void LogInformationWithParameters()
    {
        m_logger.LogInformation(LogMessageWithParameters, 10, 20);
    }

    [Benchmark]
    public void LogInformationWithLevelCheckAndParameters()
    {
        if (m_logger.IsEnabled(LogLevel.Information))
            m_logger.LogInformation(LogMessageWithParameters, 10, 20);
    }

    [Benchmark]
    public void LogInformationWithMessageDefinition()
    {
        m_logger.LogBenchmarkMessage(10, 20);
    }

    [Benchmark]
    public void LogInformationWithSourceGeneratedMessageDefinition()
    {
        m_logger.LogBenchmarkMessageGen(10, 20);
    }
}
