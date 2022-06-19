using Microsoft.Extensions.Logging;
using Snowflake;
using Snowflake.Windows;

namespace Sandbox;

internal class SandboxApp : Application
{
    public SandboxApp(ILogger<Application> logger, Func<IWindow> windowFactory, ExampleLayer exampleLayer, ImGuiLayer imGuiLayer)
        : base(logger, windowFactory) 
    {
        PushLayer(exampleLayer);
        PushOverlay(imGuiLayer);
    }
}