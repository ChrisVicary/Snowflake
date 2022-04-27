using Snowflake.Events;

namespace Snowflake.Test.Events;

[TestFixture]
public class AppRenderEventTests
{
    [Test]
    public void Name_ReturnsAppRender()
    {
        var appRenderEvent = new AppRenderEvent();
        Assert.AreEqual("AppRender", appRenderEvent.Name);
    }

    [Test]
    public void Categories_ReturnsApplication()
    {
        var appRenderEvent = new AppRenderEvent();
        Assert.AreEqual(EventCategories.Application, appRenderEvent.Categories);
    }
}
