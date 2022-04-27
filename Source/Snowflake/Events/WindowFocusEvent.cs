namespace Snowflake.Events;

public class WindowFocusEvent : Event
{
    public override string Name => "WindowFocus";

    public override EventCategories Categories => EventCategories.Application;
}
