namespace Snowflake.Events;

public class WindowResizeEvent : Event
{
    public WindowResizeEvent(uint width, uint height)
    {
        Width = width;
        Height = height;
    }

    public uint Width { get; }

    public uint Height { get; }

    public override string Name => "WindowResize";

    public override EventCategories Categories => EventCategories.Application;

    public override string ToString() => $"{base.ToString()}: {Width}, {Height}";
}
