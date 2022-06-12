using Microsoft.Extensions.Logging;
using Snowflake.Events;

namespace Snowflake;

internal static partial class LogMessageDefinitions
{
    [LoggerMessage(EventId = 0, Level = LogLevel.Trace, Message = "{e}")]
    public static partial void LogEvent(this ILogger logger, Event e);
}
