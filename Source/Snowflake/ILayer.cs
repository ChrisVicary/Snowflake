using Snowflake.Events;

namespace Snowflake;

public interface ILayer
{
    string Name { get; }
    void OnAttach();
    void OnDetach();
    void OnUpdate();
    void OnEvent(Event e);
}