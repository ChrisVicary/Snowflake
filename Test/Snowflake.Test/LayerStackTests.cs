namespace Snowflake.Test;

[TestFixture]
public class LayerStackTests
{
    [Test]
    public void PushLayer_InsertsBeforeOverlays()
    {
        var mockOverlay = new Mock<ILayer>();
        mockOverlay.SetupGet(m => m.Name)
            .Returns("Overlay");

        var mockLayer1 = new Mock<ILayer>();
        mockLayer1.SetupGet(m => m.Name)
            .Returns("Layer1");

        var mockLayer2 = new Mock<ILayer>();
        mockLayer2.SetupGet(m => m.Name)
            .Returns("Layer2");

        var layerStack = new LayerStack();
        layerStack.PushOverlay(mockOverlay.Object);
        layerStack.PushLayer(mockLayer1.Object);
        layerStack.PushLayer(mockLayer2.Object);

        CollectionAssert.AreEqual(new[] { mockLayer1.Object, mockLayer2.Object, mockOverlay.Object },
            layerStack);
    }

    [Test]
    public void PushOverlay_AddsToTheEnd()
    {
        var mockLayer = new Mock<ILayer>();
        mockLayer.SetupGet(m => m.Name)
            .Returns("Layer");

        var mockOverlay1 = new Mock<ILayer>();
        mockOverlay1.SetupGet(m => m.Name)
            .Returns("Overlay1");

        var mockOverlay2 = new Mock<ILayer>();
        mockOverlay2.SetupGet(m => m.Name)
            .Returns("Overlay2");

        var layerStack = new LayerStack();
        layerStack.PushLayer(mockLayer.Object);
        layerStack.PushOverlay(mockOverlay1.Object);
        layerStack.PushOverlay(mockOverlay2.Object);

        CollectionAssert.AreEqual(new[] { mockLayer.Object, mockOverlay1.Object, mockOverlay2.Object },
            layerStack);
    }

    [Test]
    public void PopLayer_RemovesLayer()
    {
        var mockLayer1 = new Mock<ILayer>();
        mockLayer1.SetupGet(m => m.Name)
            .Returns("Layer1");

        var mockLayer2 = new Mock<ILayer>();
        mockLayer2.SetupGet(m => m.Name)
            .Returns("Layer2");

        var mockOverlay1 = new Mock<ILayer>();
        mockOverlay1.SetupGet(m => m.Name)
            .Returns("Overlay1");

        var mockOverlay2 = new Mock<ILayer>();
        mockOverlay2.SetupGet(m => m.Name)
            .Returns("Overlay2");

        var layerStack = new LayerStack();
        layerStack.PushLayer(mockLayer1.Object);
        layerStack.PushLayer(mockLayer2.Object);
        layerStack.PushOverlay(mockOverlay1.Object);
        layerStack.PushOverlay(mockOverlay2.Object);

        layerStack.PopLayer(mockLayer1.Object);

        CollectionAssert.AreEqual(new[] { mockLayer2.Object, mockOverlay1.Object, mockOverlay2.Object },
            layerStack);
    }

    [Test]
    public void PopOverlay_RemovesOverlay()
    {
        var mockLayer1 = new Mock<ILayer>();
        mockLayer1.SetupGet(m => m.Name)
            .Returns("Layer1");

        var mockLayer2 = new Mock<ILayer>();
        mockLayer2.SetupGet(m => m.Name)
            .Returns("Layer2");

        var mockOverlay1 = new Mock<ILayer>();
        mockOverlay1.SetupGet(m => m.Name)
            .Returns("Overlay1");

        var mockOverlay2 = new Mock<ILayer>();
        mockOverlay2.SetupGet(m => m.Name)
            .Returns("Overlay2");

        var layerStack = new LayerStack();
        layerStack.PushLayer(mockLayer1.Object);
        layerStack.PushLayer(mockLayer2.Object);
        layerStack.PushOverlay(mockOverlay1.Object);
        layerStack.PushOverlay(mockOverlay2.Object);

        layerStack.PopOverlay(mockOverlay1.Object);

        CollectionAssert.AreEqual(new[] { mockLayer1.Object, mockLayer2.Object, mockOverlay2.Object },
            layerStack);
    }
}
