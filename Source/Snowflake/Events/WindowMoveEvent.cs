namespace Snowflake.Events;

public class WindowMoveEvent : Event
{
    public override string Name => "WindowMove";

    public override EventCategories Categories => EventCategories.Application;
}
