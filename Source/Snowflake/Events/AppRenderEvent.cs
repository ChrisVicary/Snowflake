namespace Snowflake.Events;

public class AppRenderEvent : Event
{
    public override string Name => "AppRender";

    public override EventCategories Categories => EventCategories.Application;
}
