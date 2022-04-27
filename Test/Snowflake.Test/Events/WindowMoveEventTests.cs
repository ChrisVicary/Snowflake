using Snowflake.Events;

namespace Snowflake.Test.Events;

[TestFixture]
public class WindowMoveEventTests
{
    [Test]
    public void Name_ReturnsWindowMove()
    {
        var windowMoveEvent = new WindowMoveEvent();
        Assert.AreEqual("WindowMove", windowMoveEvent.Name);
    }

    [Test]
    public void Categories_ReturnsApplication()
    {
        var windowMoveEvent = new WindowMoveEvent();
        Assert.AreEqual(EventCategories.Application, windowMoveEvent.Categories);
    }
}
