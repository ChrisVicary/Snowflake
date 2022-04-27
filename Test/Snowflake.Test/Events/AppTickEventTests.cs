using Snowflake.Events;

namespace Snowflake.Test.Events;

[TestFixture]
public class AppTickEventTests
{
    [Test]
    public void Name_ReturnsAppTick()
    {
        var appTickEvent = new AppTickEvent();
        Assert.AreEqual("AppTick", appTickEvent.Name);
    }

    [Test]
    public void Categories_ReturnsApplication()
    {
        var appTickEvent = new AppTickEvent();
        Assert.AreEqual(EventCategories.Application, appTickEvent.Categories);
    }
}
