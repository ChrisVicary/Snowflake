namespace Snowflake.Events;

public class AppTickEvent : Event
{
    public override string Name => "AppTick";

    public override EventCategories Categories => EventCategories.Application;
}
