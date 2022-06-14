
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

    [Test]
    public void RunLoop_UpdatesLayers()
    {
        var mockLogger = new Mock<ILogger<Application>>();
        var mockWindow = new Mock<IWindow>();
        var mockApplication = new Mock<Application>(mockLogger.Object, () => mockWindow.Object) { CallBase = true };

        var updatedLayers = new List<string>();

        var mockLayer1 = new Mock<ILayer>();
        mockLayer1.SetupGet(m => m.Name)
            .Returns("Layer1");
        mockLayer1.Setup(m => m.OnUpdate())
            .Callback(() => updatedLayers.Add("Layer1"));
        
        var mockLayer2 = new Mock<ILayer>();
        mockLayer2.SetupGet(m => m.Name)
            .Returns("Layer2");
        mockLayer2.Setup(m => m.OnUpdate())
            .Callback(() => updatedLayers.Add("Layer2"));

        var mockOverlay1 = new Mock<ILayer>();
        mockOverlay1.SetupGet(m => m.Name)
            .Returns("Overlay1");
        mockOverlay1.Setup(m => m.OnUpdate())
            .Callback(() => updatedLayers.Add("Overlay1"));

        var mockOverlay2 = new Mock<ILayer>();
        mockOverlay2.SetupGet(m => m.Name)
            .Returns("Overlay2");
        mockOverlay2.Setup(m => m.OnUpdate())
            .Callback(() => updatedLayers.Add("Overlay2"));

        mockApplication.Object.PushLayer(mockLayer1.Object);
        mockApplication.Object.PushLayer(mockLayer2.Object);
        mockApplication.Object.PushOverlay(mockOverlay1.Object);
        mockApplication.Object.PushOverlay(mockOverlay2.Object);

        Action<Event>? onEventAction = null;
        mockWindow.Setup(m => m.SetEventCallback(It.IsAny<Action<Event>>()))
            .Callback<Action<Event>>(a => onEventAction = a);
        mockWindow.Setup(m => m.OnUpdate())
            .Callback(() => onEventAction?.Invoke(new WindowCloseEvent()));

        mockApplication.Object.Run();

        mockLayer1.Verify(m => m.OnUpdate(), Times.Once());
        mockLayer2.Verify(m => m.OnUpdate(), Times.Once());
        mockOverlay1.Verify(m => m.OnUpdate(), Times.Once());
        mockOverlay2.Verify(m => m.OnUpdate(), Times.Once());
        CollectionAssert.AreEqual(new string[] { "Layer1", "Layer2", "Overlay1", "Overlay2" }, updatedLayers);
    }

    [Test]
    public void WindowEvents_PassedToLayers_InReverse()
    {
        var mockLogger = new Mock<ILogger<Application>>();
        var mockWindow = new Mock<IWindow>();
        var mockApplication = new Mock<Application>(mockLogger.Object, () => mockWindow.Object) { CallBase = true };

        var updatedLayers = new List<string>();

        var mockLayer1 = new Mock<ILayer>();
        mockLayer1.SetupGet(m => m.Name)
            .Returns("Layer1");
        mockLayer1.Setup(m => m.OnEvent(It.IsAny<Event>()))
            .Callback(() => updatedLayers.Add("Layer1"));

        var mockLayer2 = new Mock<ILayer>();
        mockLayer2.SetupGet(m => m.Name)
            .Returns("Layer2");
        mockLayer2.Setup(m => m.OnEvent(It.IsAny<Event>()))
            .Callback(() => updatedLayers.Add("Layer2"));

        var mockOverlay1 = new Mock<ILayer>();
        mockOverlay1.SetupGet(m => m.Name)
            .Returns("Overlay1");
        mockOverlay1.Setup(m => m.OnEvent(It.IsAny<Event>()))
            .Callback(() => updatedLayers.Add("Overlay1"));

        var mockOverlay2 = new Mock<ILayer>();
        mockOverlay2.SetupGet(m => m.Name)
            .Returns("Overlay2");
        mockOverlay2.Setup(m => m.OnEvent(It.IsAny<Event>()))
            .Callback(() => updatedLayers.Add("Overlay2"));

        mockApplication.Object.PushLayer(mockLayer1.Object);
        mockApplication.Object.PushLayer(mockLayer2.Object);
        mockApplication.Object.PushOverlay(mockOverlay1.Object);
        mockApplication.Object.PushOverlay(mockOverlay2.Object);

        Action<Event>? onEventAction = null;
        mockWindow.Setup(m => m.SetEventCallback(It.IsAny<Action<Event>>()))
            .Callback<Action<Event>>(a => onEventAction = a);
        mockWindow.Setup(m => m.OnUpdate())
            .Callback(() =>
            {
                onEventAction?.Invoke(new AppUpdateEvent());
                onEventAction?.Invoke(new WindowCloseEvent());
            });

        mockApplication.Object.Run();

        mockLayer1.Verify(m => m.OnEvent(It.IsAny<Event>()), Times.Once());
        mockLayer2.Verify(m => m.OnEvent(It.IsAny<Event>()), Times.Once());
        mockOverlay1.Verify(m => m.OnEvent(It.IsAny<Event>()), Times.Once());
        mockOverlay2.Verify(m => m.OnEvent(It.IsAny<Event>()), Times.Once());
        CollectionAssert.AreEqual(new string[] { "Overlay2", "Overlay1", "Layer2", "Layer1" }, updatedLayers);
    }

    [Test]
    public void WindowEvents_StopsPassingToLayers_WhenHandled()
    {
        var mockLogger = new Mock<ILogger<Application>>();
        var mockWindow = new Mock<IWindow>();
        var mockApplication = new Mock<Application>(mockLogger.Object, () => mockWindow.Object) { CallBase = true };

        var updatedLayers = new List<string>();

        var mockLayer1 = new Mock<ILayer>();
        mockLayer1.SetupGet(m => m.Name)
            .Returns("Layer1");
        mockLayer1.Setup(m => m.OnEvent(It.IsAny<Event>()))
            .Callback(() => updatedLayers.Add("Layer1"));

        var mockLayer2 = new Mock<ILayer>();
        mockLayer2.SetupGet(m => m.Name)
            .Returns("Layer2");
        mockLayer2.Setup(m => m.OnEvent(It.IsAny<Event>()))
            .Callback(() => updatedLayers.Add("Layer2"));

        var mockOverlay1 = new Mock<ILayer>();
        mockOverlay1.SetupGet(m => m.Name)
            .Returns("Overlay1");
        mockOverlay1.Setup(m => m.OnEvent(It.IsAny<Event>()))
            .Callback<Event>(e =>
            {
                e.Handled = true;
                updatedLayers.Add("Overlay1");
            });

        var mockOverlay2 = new Mock<ILayer>();
        mockOverlay2.SetupGet(m => m.Name)
            .Returns("Overlay2");
        mockOverlay2.Setup(m => m.OnEvent(It.IsAny<Event>()))
            .Callback(() => updatedLayers.Add("Overlay2"));

        mockApplication.Object.PushLayer(mockLayer1.Object);
        mockApplication.Object.PushLayer(mockLayer2.Object);
        mockApplication.Object.PushOverlay(mockOverlay1.Object);
        mockApplication.Object.PushOverlay(mockOverlay2.Object);

        Action<Event>? onEventAction = null;
        mockWindow.Setup(m => m.SetEventCallback(It.IsAny<Action<Event>>()))
            .Callback<Action<Event>>(a => onEventAction = a);
        mockWindow.Setup(m => m.OnUpdate())
            .Callback(() =>
            {
                onEventAction?.Invoke(new AppUpdateEvent());
                onEventAction?.Invoke(new WindowCloseEvent());
            });

        mockApplication.Object.Run();

        mockLayer1.Verify(m => m.OnEvent(It.IsAny<Event>()), Times.Never());
        mockLayer2.Verify(m => m.OnEvent(It.IsAny<Event>()), Times.Never());
        mockOverlay1.Verify(m => m.OnEvent(It.IsAny<Event>()), Times.Once());
        mockOverlay2.Verify(m => m.OnEvent(It.IsAny<Event>()), Times.Once());
        CollectionAssert.AreEqual(new string[] { "Overlay2", "Overlay1" }, updatedLayers);
    }
}