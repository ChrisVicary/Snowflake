using Snowflake.Events;

namespace Snowflake.Test.Events;

[TestFixture]
public class WindowCloseEventTests
{
    [Test]
    public void Name_ReturnsWindowClose()
    {
        var windowCloseEvent = new WindowCloseEvent();
        Assert.AreEqual("WindowClose", windowCloseEvent.Name);
    }

    [Test]
    public void Categories_ReturnsApplication()
    {
        var windowCloseEvent = new WindowCloseEvent();
        Assert.AreEqual(EventCategories.Application, windowCloseEvent.Categories);
    }
}
