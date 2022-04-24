using Microsoft.Extensions.Logging;

namespace Snowflake.Benchmark.Logging;

internal static class LoggerMessageDefinitions
{
    private static readonly Action<ILogger, int, int, Exception?> BenchmarkLogMessageDefinition =
        LoggerMessage.Define<int, int>(LogLevel.Information, 0, "This is a log message with parameters {first} and {second}.");

    public static void LogBenchmarkMessage(this ILogger logger, int first, int second) =>
        BenchmarkLogMessageDefinition(logger, first, second, null);
}

internal static partial class LoggerMessageDefinitionsGen
{
    [LoggerMessage(EventId = 0,Level = LogLevel.Information, Message = "This is a log message with parameters {first} and {second}.")]
    public static partial void LogBenchmarkMessageGen(this ILogger logger, int first, int second);
}
