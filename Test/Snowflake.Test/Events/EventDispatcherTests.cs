using Snowflake.Events;

namespace Snowflake.Test.Events;

[TestFixture]
public class EventDispatcherTests
{
    [Test]
    public void Dispatch_WhenEventTypeMatches_CallsFuncAndReturnsTrue()
    {
        var testEvent = new TestEvent();
        var eventDispatcher = new EventDispatcher(testEvent);

        bool functionCalled = false;
        bool result = eventDispatcher.Dispatch<TestEvent>(e =>
        {
            functionCalled = true;
            return true;
        });

        Assert.IsTrue(result);
        Assert.IsTrue(functionCalled);
        Assert.IsTrue(testEvent.Handled);
    }

    [Test]
    public void Dispatch_WhenEventTypeDoesNotMatch_DoesNotCallFuncAndReturnsFalse()
    {
        var testEvent = new TestEvent();
        var eventDispatcher = new EventDispatcher(testEvent);

        bool functionCalled = false;
        bool result = eventDispatcher.Dispatch<InvalidTestEvent>(e =>
        {
            functionCalled = true;
            return true;
        });

        Assert.IsFalse(result);
        Assert.IsFalse(functionCalled);
        Assert.IsFalse(testEvent.Handled);
    }

    private class TestEvent : Event
    {
        public override string Name => throw new NotImplementedException();

        public override EventCategories Categories => throw new NotImplementedException();
    }

    private class InvalidTestEvent : Event
    {
        public override string Name => throw new NotImplementedException();

        public override EventCategories Categories => throw new NotImplementedException();
    }
}
