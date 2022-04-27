using Snowflake.Events;

namespace Snowflake.Test.Events;

[TestFixture]
public class KeyPressEventTests
{
    [Test]
    public void Constructor_SetsKeyCodeAndRepeatCount()
    {
        var keyPressEvent = new KeyPressEvent(42, 420);
        Assert.AreEqual(42, keyPressEvent.KeyCode);
        Assert.AreEqual(420, keyPressEvent.RepeatCount);
    }

    [Test]
    public void Name_ReturnsKeyPress()
    {
        var keyPressEvent = new KeyPressEvent(42, 420);
        Assert.AreEqual("KeyPress", keyPressEvent.Name);
    }

    [Test]
    public void ToString_ReturnsNameKeyCodeAndRepeatCount()
    {
        var keyPressEvent = new KeyPressEvent(42, 420);
        Assert.AreEqual("KeyPress: 42 (420 repeats)", keyPressEvent.ToString());
    }
}
