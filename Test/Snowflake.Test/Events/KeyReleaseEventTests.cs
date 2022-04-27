using Snowflake.Events;

namespace Snowflake.Test.Events;

[TestFixture]
public class KeyReleaseEventTests
{
    [Test]
    public void Constructor_SetsKeyCode()
    {
        var keyReleaseEvent = new KeyReleaseEvent(42);
        Assert.AreEqual(42, keyReleaseEvent.KeyCode);
    }

    [Test]
    public void Name_ReturnsKeyRelease()
    {
        var keyReleaseEvent = new KeyReleaseEvent(42);
        Assert.AreEqual("KeyRelease", keyReleaseEvent.Name);
    }
}
