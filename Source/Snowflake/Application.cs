using Microsoft.Extensions.Logging;
using Snowflake.Events;

namespace Snowflake;

public abstract class Application
{
    private readonly ILogger<Application> m_logger;
    private readonly Func<IWindow> m_windowFactory;

    private IWindow? m_window;
    private bool m_running = true;

    public Application(ILogger<Application> logger, Func<IWindow> windowFactory)
    {
        m_logger = logger;
        m_windowFactory = windowFactory;
    }

    public virtual void Run()
    {
        m_logger.LogInformation("Snowflake Initialized.");

        m_window = m_windowFactory();
        m_window.SetEventCallback(OnEvent);
        m_logger.LogInformation("Window Created.");

        while (m_running)
        {
            m_window.OnUpdate();
        }
    }

    protected virtual void OnEvent(Event e)
    {
        var dispatcher = new EventDispatcher(e);
        dispatcher.Dispatch<WindowCloseEvent>(OnWindowClose);

        m_logger.LogEvent(e);
    }

    private bool OnWindowClose(WindowCloseEvent e)
    {
        m_running = false;
        return true;
    }
}
