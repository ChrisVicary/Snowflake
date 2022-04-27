using Snowflake.Events;

namespace Snowflake.Test.Events;

[TestFixture]
public class MouseScrollEventTests
{
    [Test]
    public void Constructor_SetsXOffsetAndYOffset()
    {
        var mouseScrollEvent = new MouseScrollEvent(42f, 420f);
        Assert.AreEqual(42f, mouseScrollEvent.XOffset);
        Assert.AreEqual(420f, mouseScrollEvent.YOffset);
    }

    [Test]
    public void Name_ReturnsMouseScroll()
    {
        var mouseScrollEvent = new MouseScrollEvent(42f, 420f);
        Assert.AreEqual("MouseScroll", mouseScrollEvent.Name);
    }

    [Test, Sequential]
    public void Categories_ReturnsInputAndMouse([Values] EventCategories eventCategories, [Values(true, false, true, false, true, false)] bool hasFlag)
    {
        var mouseScrollEvent = new MouseScrollEvent(42f, 420f);
        Assert.AreEqual(hasFlag, mouseScrollEvent.Categories.HasFlag(eventCategories));
    }

    [Test]
    public void ToString_ReturnsNameXOffsetAndYOffset()
    {
        var mouseScrollEvent = new MouseScrollEvent(42f, 420f);
        Assert.AreEqual("MouseScroll: 42, 420", mouseScrollEvent.ToString());
    }
}
