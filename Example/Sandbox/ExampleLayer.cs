using Microsoft.Extensions.Logging;
using Snowflake;
using Snowflake.Events;

namespace Sandbox;

internal class ExampleLayer : Layer
{
    private ILogger<ExampleLayer> m_logger;

    public ExampleLayer(ILogger<ExampleLayer> logger) 
        : base("Example")
    {
        m_logger = logger;
    }

    public override void OnUpdate() => m_logger.LogInformation("Update");

    public override void OnEvent(Event e) => m_logger.LogEvent(e);
}
