namespace Snowflake.Events;

public class EventDispatcher
{
    private readonly Event m_event;

    public EventDispatcher(Event @event)
    {
        m_event = @event;
    }

    public bool Dispatch<T>(Func<T, bool> func) where T : Event
    {
        if (m_event is T typedEvent)
        {
            m_event.Handled = func(typedEvent);
            return true;
        }

        return false;
    }
}
