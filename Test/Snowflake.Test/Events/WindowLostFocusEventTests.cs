using Snowflake.Events;

namespace Snowflake.Test.Events;

[TestFixture]
public class WindowLostFocusEventTests
{
    [Test]
    public void Name_ReturnsWindowLostFocus()
    {
        var windowLostFocusEvent = new WindowLostFocusEvent();
        Assert.AreEqual("WindowLostFocus", windowLostFocusEvent.Name);
    }

    [Test]
    public void Categories_ReturnsApplication()
    {
        var windowLostFocusEvent = new WindowLostFocusEvent();
        Assert.AreEqual(EventCategories.Application, windowLostFocusEvent.Categories);
    }
}
