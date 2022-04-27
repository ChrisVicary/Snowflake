using Snowflake.Events;

namespace Snowflake.Test.Events;

[TestFixture]
public class WindowFocusEventTests
{
    [Test]
    public void Name_ReturnsWindowFocus()
    {
        var windowFocusEvent = new WindowFocusEvent();
        Assert.AreEqual("WindowFocus", windowFocusEvent.Name);
    }

    [Test]
    public void Categories_ReturnsApplication()
    {
        var windowFocusEvent = new WindowFocusEvent();
        Assert.AreEqual(EventCategories.Application, windowFocusEvent.Categories);
    }
}
