using Snowflake.Events;

namespace Snowflake.Test.Events;

[TestFixture]
public class KeyEventTests
{
    [Test]
    public void Constructor_SetsKeyCode()
    {
        var mockKeyEvent = new Mock<KeyEvent>(42) { CallBase = true };
        Assert.AreEqual(42, mockKeyEvent.Object.KeyCode);
    }

    [Test, Sequential]
    public void Categories_ReturnsInputAndKeyboard([Values] EventCategories eventCategories, [Values(true, false, true, true, false, false)] bool hasFlag)
    {
        var mockKeyEvent = new Mock<KeyEvent>(42) { CallBase = true };
        Assert.AreEqual(hasFlag, mockKeyEvent.Object.Categories.HasFlag(eventCategories));
    }

    [Test]
    public void ToString_ReturnsNameAndKeyCode()
    {
        var mockKeyEvent = new Mock<KeyEvent>(42) { CallBase = true };
        mockKeyEvent.SetupGet(m => m.Name).Returns("MockKeyEvent");
        Assert.AreEqual("MockKeyEvent: 42", mockKeyEvent.Object.ToString());
    }
}
