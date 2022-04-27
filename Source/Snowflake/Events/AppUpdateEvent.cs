namespace Snowflake.Events;

public class AppUpdateEvent : Event
{
    public override string Name => "AppUpdate";

    public override EventCategories Categories => EventCategories.Application;
}
