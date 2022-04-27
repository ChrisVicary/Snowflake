using Snowflake.Events;

namespace Snowflake.Test.Events;

[TestFixture]
public class MouseMoveEventTests
{
    [Test]
    public void Constructor_SetsXAndY()
    {
        var mouseMoveEvent = new MouseMoveEvent(42f, 420f);
        Assert.AreEqual(42f, mouseMoveEvent.X);
        Assert.AreEqual(420f, mouseMoveEvent.Y);
    }

    [Test]
    public void Name_ReturnsMouseMove()
    {
        var mouseMoveEvent = new MouseMoveEvent(42f, 420f);
        Assert.AreEqual("MouseMove", mouseMoveEvent.Name);
    }

    [Test, Sequential]
    public void Categories_ReturnsInputAndMouse([Values] EventCategories eventCategories, [Values(true, false, true, false, true, false)] bool hasFlag)
    {
        var mouseMoveEvent = new MouseMoveEvent(42f, 420f);
        Assert.AreEqual(hasFlag, mouseMoveEvent.Categories.HasFlag(eventCategories));
    }

    [Test]
    public void ToString_ReturnsNameXAndY()
    {
        var mouseMoveEvent = new MouseMoveEvent(42f, 420f);
        Assert.AreEqual("MouseMove: 42, 420", mouseMoveEvent.ToString());
    }
}
