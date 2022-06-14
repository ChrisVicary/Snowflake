namespace Snowflake.Test;

[TestFixture]
public class LayerTests
{
    [Test]
    public void Constructor_SetsName()
    {
        var layerName = "TestLayer";
        var mockLayer = new Mock<Layer>(layerName) { CallBase = true };

        var layer = mockLayer.Object;

        Assert.AreEqual(layerName, layer.Name);
    }
}
