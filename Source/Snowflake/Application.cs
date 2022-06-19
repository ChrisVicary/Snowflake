using Microsoft.Extensions.Logging;
using Snowflake.Events;

namespace Snowflake;

public abstract class Application
{
    private static Application? m_instance;

    private readonly ILogger<Application> m_logger;
    private readonly Func<IWindow> m_windowFactory;
    private readonly LayerStack m_layerStack = new LayerStack();

    private IWindow? m_window;
    private bool m_running = true;

    public Application(ILogger<Application> logger, Func<IWindow> windowFactory)
    {
        m_logger = logger;
        m_windowFactory = windowFactory;
        m_window = m_windowFactory();

        m_instance = this;
    }

    public static Application? Get() => m_instance;

    public IWindow? Window => m_window;

    public void PushLayer(ILayer layer) => m_layerStack.PushLayer(layer);

    public void PushOverlay(ILayer layer) => m_layerStack.PushOverlay(layer); 

    public virtual void Run()
    {
        m_logger.LogInformation("Snowflake Initialized.");

        if (m_window == null)
            return;

        m_window.SetEventCallback(OnEvent);
        m_logger.LogInformation("Window Created.");

        while (m_running)
        {
            foreach (var layer in m_layerStack)
                layer.OnUpdate();

            m_window.OnUpdate();
        }
    }

    protected virtual void OnEvent(Event e)
    {
        var dispatcher = new EventDispatcher(e);
        dispatcher.Dispatch<WindowCloseEvent>(OnWindowClose);

        foreach(var layer in m_layerStack.Reverse())
        {
            if (e.Handled)
                break;

            layer.OnEvent(e);
        }
    }

    private bool OnWindowClose(WindowCloseEvent e)
    {
        m_running = false;
        return true;
    }
}
