using Snowflake.Events;

namespace Snowflake.Test.Events;

[TestFixture]
public class AppUpdateEventTests
{
    [Test]
    public void Name_ReturnsAppUpdate()
    {
        var appUpdateEvent = new AppUpdateEvent();
        Assert.AreEqual("AppUpdate", appUpdateEvent.Name);
    }

    [Test]
    public void Categories_ReturnsApplication()
    {
        var appUpdateEvent = new AppUpdateEvent();
        Assert.AreEqual(EventCategories.Application, appUpdateEvent.Categories);
    }
}
