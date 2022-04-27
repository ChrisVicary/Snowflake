namespace Snowflake.Events;

public abstract class KeyEvent : Event
{
    protected KeyEvent(int keyCode)
    {
        KeyCode = keyCode;
    }

    public int KeyCode { get; }

    public override EventCategories Categories => EventCategories.Input | EventCategories.Keyboard;

    public override string ToString() => $"{base.ToString()}: {KeyCode}";
}
