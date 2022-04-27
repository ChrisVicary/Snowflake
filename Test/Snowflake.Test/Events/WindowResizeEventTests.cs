using Snowflake.Events;

namespace Snowflake.Test.Events;

[TestFixture]
public class WindowResizeEventTests
{
    [Test]
    public void Constructor_SetsWidthAndHeight()
    {
        var windowResizeEvent = new WindowResizeEvent(42u, 420u);
        Assert.AreEqual(42u, windowResizeEvent.Width);
        Assert.AreEqual(420u, windowResizeEvent.Height);
    }

    [Test]
    public void Name_ReturnsWindowResize()
    {
        var windowResizeEvent = new WindowResizeEvent(42u, 420u);
        Assert.AreEqual("WindowResize", windowResizeEvent.Name);
    }

    [Test]
    public void Categories_ReturnsApplication()
    {
        var windowResizeEvent = new WindowResizeEvent(42u, 420u);
        Assert.AreEqual(EventCategories.Application, windowResizeEvent.Categories);
    }

    [Test]
    public void ToString_ReturnsNameWidthAndHeight()
    {
        var windowResizeEvent = new WindowResizeEvent(42u, 420u);
        Assert.AreEqual("WindowResize: 42, 420", windowResizeEvent.ToString());
    }
}
