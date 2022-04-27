namespace Snowflake.Events;

public class WindowLostFocusEvent : Event
{
    public override string Name => "WindowLostFocus";

    public override EventCategories Categories => EventCategories.Application;
}
