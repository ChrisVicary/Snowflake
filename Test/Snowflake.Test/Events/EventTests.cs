using Snowflake.Events;

namespace Snowflake.Test.Events;

[TestFixture]
public class EventTests
{
    [Test]
    public void IsInCategory_WhenSingleFlagSet_ReturnsTrue([Values]EventCategories eventCategories)
    {
        var mockEvent = new Mock<Event>() { CallBase = true };
        mockEvent.SetupGet(m => m.Categories).Returns(eventCategories);
        Assert.IsTrue(mockEvent.Object.IsInCategory(eventCategories));
    }

    [Test]
    public void ToString_ReturnsName()
    {
        var mockEvent = new Mock<Event>() { CallBase = true };
        mockEvent.SetupGet(m => m.Name).Returns("MockEvent");
        Assert.AreEqual("MockEvent", mockEvent.Object.ToString());
    }
}
