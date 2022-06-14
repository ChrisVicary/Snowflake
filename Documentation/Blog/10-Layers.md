# Layers

## ILayer Interface

My C# implementation of layers is fairly close to the original C++ one of the main differences is adding the interface and abstract base class.

```cs
public interface ILayer
{
    string Name { get; }
    void OnAttach();
    void OnDetach();
    void OnUpdate();
    void OnEvent(Event e);
}
```

## LayerStack Class

Similarly the `LayerStack` implementation is also very close to the C++ implementation I use a `List<ILayer>` for the vector, the code it actually a little bit cleaner in C# as there are functions on the `List<T>` class to remove elements. By implementing `IEnumerable<T>` on the `LayerStack` class the any code that needs to traverse the layers can just use a `foreach` loop directly.

```cs
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
```

## Application

The only interesting thing to note about the usage of the `LayerStack` class in the `Application` is that I can use LINQ `Reverse()` method to iterate through the layers backwards to send the events to the layers.

```cs
public abstract class Application
{
    private readonly LayerStack m_layerStack = new LayerStack();

    ...

    public void PushLayer(ILayer layer) => m_layerStack.PushLayer(layer);

    public void PushOverlay(ILayer layer) => m_layerStack.PushOverlay(layer); 

    public virtual void Run()
    {
        ...
        while (m_running)
        {
            foreach (var layer in m_layerStack)
                layer.OnUpdate();

            m_window.OnUpdate();
        }
    }

    protected virtual void OnEvent(Event e)
    {
        ...
        foreach(var layer in m_layerStack.Reverse())
        {
            layer.OnEvent(e);
            if (e.Handled)
                break;
        }
    }
    ...
}
```

Adding the `PushLayer()` and `PushOverlay()` methods on the `Application` was something of a surprise to me. I wouldn't have expected other parts of the system to always have access to the `Application` class, or require access to the `Application` to be able to interact with layers. I would understand if the methods were protected and only classes derived from `Application` were allowed to add layers. I would probably have something like a `LayerService` that would expose the current layers, and the methods to add and remove separate from the `Application` class. This kind of architectur is more common in C# when using dependency injection and inversion of control containers, and is a refactoring that I might consider in the future. For now I will stick with the existing implementation and see it is used in used in the future before I consider refactoring.

## Tests

As with my other updates, I've added new unit tests for the `Layer` and `LayerStack` classes and added more tests to the `Application` class. Interestingly writing a test for the event handling in the layer uncovered a bug in the original code, where the `WindowCloseEvent` which is marked as handled by the `Application` itself is still passed to the first layer in the stack before hitting the `break`. By moving the handled check before the first layer `OnEvent()` call we early out of the loop before any layers are called. Looking ahead at the most recent Hazel code I can see that this change was made there at some point as well.

```cs
    protected virtual void OnEvent(Event e)
    {
        ...
        foreach(var layer in m_layerStack.Reverse())
        {
            if (e.Handled)
                break;

            layer.OnEvent(e);
        }
    }
```

## Video Link

[TheCherno - Game Engine Series - Layers](https://www.youtube.com/watch?v=r74WxFMIEdU&list=PLlrATfBNZ98dC-V-N3m0Go4deliWHPFwT&index=13&ab_channel=TheCherno)

## Next
[Modern OpenGL](https://github.com/ChrisVicary/Snowflake/blob/main/Documentation/Blog/11-ModernOpenGL.md)