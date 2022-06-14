using System.Collections;

namespace Snowflake;

internal class LayerStack : IEnumerable<ILayer>
{
    private List<ILayer> m_layers = new List<ILayer>();
    private int m_layerInsert = 0;

    public void PushLayer(ILayer layer) => m_layers.Insert(m_layerInsert++, layer);

    public void PushOverlay(ILayer layer) => m_layers.Add(layer);

    public void PopLayer(ILayer layer)
    {
        if (m_layers.Remove(layer))
            m_layerInsert--;
    }

    public void PopOverlay(ILayer layer) => m_layers.Remove(layer); 

    public IEnumerator<ILayer> GetEnumerator() => m_layers.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}