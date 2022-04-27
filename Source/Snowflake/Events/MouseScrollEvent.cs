namespace Snowflake.Events;

public class MouseScrollEvent : Event
{
    public MouseScrollEvent(float xOffset, float yOffset)
    {
        XOffset = xOffset;
        YOffset = yOffset;
    }

    public float XOffset { get; }

    public float YOffset { get; }

    public override string Name => "MouseScroll";

    public override EventCategories Categories => EventCategories.Input | EventCategories.Mouse;

    public override string ToString() => $"{base.ToString()}: {XOffset}, {YOffset}";
}
