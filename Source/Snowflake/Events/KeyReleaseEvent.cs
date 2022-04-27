namespace Snowflake.Events;

public class KeyReleaseEvent : KeyEvent
{
    public KeyReleaseEvent(int keyCode)
        :base(keyCode)
    {
    }

    public override string Name => "KeyRelease";
}
