namespace Snowflake.Events;

public class MouseMoveEvent : Event
{
    public MouseMoveEvent(float x, float y)
    {
        X = x;
        Y = y;
    }

    public float X { get; }

    public float Y { get; }

    public override string Name => "MouseMove";

    public override EventCategories Categories => EventCategories.Input | EventCategories.Mouse;

    public override string ToString() => $"{base.ToString()}: {X}, {Y}";
}
