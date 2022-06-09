namespace Snowflake;

public class WindowProps
{
    public WindowProps(string title = "Snowflake Engine", uint width = 1280, uint height = 720)
    {
        Title = title;
        Width = width;
        Height = height;
    }

    public string Title { get; }
    public uint Width { get; }
    public uint Height { get; }
}
