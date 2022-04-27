namespace Snowflake.Events;

public class MouseButtonReleaseEvent : MouseButtonEvent
{
    public MouseButtonReleaseEvent(int button)
        : base(button)
    {
    }

    public override string Name => "MouseButtonRelease";
}
