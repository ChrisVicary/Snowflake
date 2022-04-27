namespace Snowflake.Events;

public class KeyPressEvent : KeyEvent
{
    public KeyPressEvent(int keyCode, int repeatCount)
        : base(keyCode)
    {
        RepeatCount = repeatCount;
    }

    public int RepeatCount { get; }

    public override string Name => "KeyPress";

    public override string ToString() => $"{base.ToString()} ({RepeatCount} repeats)";
}
