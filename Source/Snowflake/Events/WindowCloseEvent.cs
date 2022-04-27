namespace Snowflake.Events;

public class WindowCloseEvent : Event
{
    public override string Name => "WindowClose";

    public override EventCategories Categories => EventCategories.Application;
}
