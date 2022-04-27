namespace Snowflake.Events;

public abstract class MouseButtonEvent : Event
{
    protected MouseButtonEvent(int button)
    {
        MouseButton = button;
    }

    public int MouseButton { get; }

    public override EventCategories Categories => EventCategories.Input | EventCategories.Mouse | EventCategories.MouseButton;

    public override string ToString() => $"{base.ToString()}: {MouseButton}";
}
