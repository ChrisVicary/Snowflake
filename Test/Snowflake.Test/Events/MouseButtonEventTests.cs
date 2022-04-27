using Snowflake.Events;

namespace Snowflake.Test.Events;

[TestFixture]
public class MouseButtonEventTests
{
    [Test]
    public void Constructor_SetsMouseButton()
    {
        var mockMouseButtonEvent = new Mock<MouseButtonEvent>(42) { CallBase = true };
        Assert.AreEqual(42, mockMouseButtonEvent.Object.MouseButton);
    }

    [Test, Sequential]
    public void Categories_ReturnsInputAndMouse([Values] EventCategories eventCategories, [Values(true, false, true, false, true, true)] bool hasFlag)
    {
        var mockMouseButtonEvent = new Mock<MouseButtonEvent>(42) { CallBase = true };
        Assert.AreEqual(hasFlag, mockMouseButtonEvent.Object.Categories.HasFlag(eventCategories));
    }

    [Test]
    public void ToString_ReturnsNameAndMouseButton()
    {
        var mockMouseButtonEvent = new Mock<MouseButtonEvent>(42) { CallBase = true };
        mockMouseButtonEvent.SetupGet(m => m.Name).Returns("MockMouseButtonEvent");
        Assert.AreEqual("MockMouseButtonEvent: 42", mockMouseButtonEvent.Object.ToString());
    }
}
