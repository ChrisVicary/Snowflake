using Snowflake.Events;

namespace Snowflake;

public abstract class Layer : ILayer
{
    protected Layer(string name = "Layer")
    {
        Name = name;
    }

    public string Name { get; }

    public virtual void OnAttach() { }
    public virtual void OnDetach() { }
    public virtual void OnUpdate() { }
    public virtual void OnEvent(Event e) { }
}