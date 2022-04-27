using Snowflake.Events;

namespace Snowflake.Test.Events;

[TestFixture]
public class MouseButtonReleaseEventTests
{
    [Test]
    public void Constructor_SetsMouseButton()
    {
        var mouseButtonReleaseEvent = new MouseButtonReleaseEvent(42);
        Assert.AreEqual(42, mouseButtonReleaseEvent.MouseButton);
    }

    [Test]
    public void Name_ReturnsMouseButtonRelease()
    {
        var mouseButtonReleaseEvent = new MouseButtonReleaseEvent(42);
        Assert.AreEqual("MouseButtonRelease", mouseButtonReleaseEvent.Name);
    }
}
