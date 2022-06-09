using Snowflake.Events;

namespace Snowflake;

public interface IWindow
{
    uint Width { get; }
    uint Height { get; }

    void OnUpdate();

    void SetEventCallback(Action<Event> callback);
    void SetVSync(bool enabled);
    bool IsVSync();
}
