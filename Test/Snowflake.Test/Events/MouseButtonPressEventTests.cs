using Snowflake.Events;

namespace Snowflake.Test.Events;

[TestFixture]
public class MouseButtonPressEventTests
{
    [Test]
    public void Constructor_SetsMouseButton()
    {
        var mouseButtonPressEvent = new MouseButtonPressEvent(42);
        Assert.AreEqual(42, mouseButtonPressEvent.MouseButton);
    }

    [Test]
    public void Name_ReturnsMouseButtonPress()
    {
        var mouseButtonPressEvent = new MouseButtonPressEvent(42);
        Assert.AreEqual("MouseButtonPress", mouseButtonPressEvent.Name);
    }
}
