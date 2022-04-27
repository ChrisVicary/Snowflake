namespace Snowflake.Events;

public class MouseButtonPressEvent : MouseButtonEvent
{
    public MouseButtonPressEvent(int button) 
        : base(button)
    {
    }

    public override string Name => "MouseButtonPress";
}
