
using Microsoft.Extensions.Logging;
using Snowflake.Events;

namespace Snowflake.Test;

[TestFixture]
public class ApplicationTests
{
    [Test]
    public void WindowCloseEvent_StopsApplication()
    {
        var mockLogger = new Mock<ILogger<Application>>();
        var mockWindow = new Mock<IWindow>();
        var mockApplication = new Mock<Application>(mockLogger.Object, () => mockWindow.Object) { CallBase = true };

        Action<Event>? onEventAction = null;
        mockWindow.Setup(m => m.SetEventCallback(It.IsAny<Action<Event>>()))
            .Callback<Action<Event>>(a => onEventAction = a);
        mockWindow.Setup(m => m.OnUpdate())
            .Callback(() => onEventAction?.Invoke(new WindowCloseEvent()));

        mockApplication.Object.Run();

        Assert.IsNotNull(onEventAction);
        mockWindow.Verify(m => m.SetEventCallback(It.IsAny<Action<Event>>()), Times.Once());
        mockWindow.Verify(m => m.OnUpdate(), Times.Once());
    }
}