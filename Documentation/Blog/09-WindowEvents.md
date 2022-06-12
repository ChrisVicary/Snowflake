# Window Events

## Application Event Callback

Adding the `OnEvent` method in the `Application` class and passing it to the window was straight forward. In C# I don't need to worry about the `bind` syntax and the method automatically matches the `Action<Event>` parameter of the `SetEventCallback` method. I also decided to make the `OnEvent` method protected, it didn't seem like something I would want to get called from other parts of the application.

```cs
public abstract class Application
{
    ...
    public virtual void Run()
    {
        ...
        m_window.SetEventCallback(OnEvent);
        ...
    }

    protected virtual void OnEvent(Event e)
    {
        m_logger.LogEvent(e);
    }
}
```

Notice that I use a `LogEvent()` method for logging the event. This is using a new extension method I created, following on from my best practice for high performance logging. I have created a new static class in which to define any log messages.

```cs
internal static partial class LogMessageDefinitions
{
    [LoggerMessage(EventId = 0, Level = LogLevel.Trace, Message = "{e}")]
    public static partial void LogEvent(this ILogger logger, Event e);
}
```

## Lambdas in C++/CLI

Setting the GLFW callbacks in the video series was done using lambdas, when I attempted this in C++/CLI I received an error, "a local lambda is not allowed in a member function of a managed class", after a bit of googling it became apparent that C++ lambdas and managed classes just don't play nicely together. To work around this I needed to create a delegate that matched the callback signature and function to instantiate the delegate. I would then be able to create a `GCHandle` to the delegate in managed memory and marshal a native function pointer to that handle to pass to the GLFW callback.

```c++
delegate void WindowSizeDelegate(GLFWwindow* window, int width, int height);

void WindowSizeCallback(GLFWwindow* window, int width, int height) { }

void WindowsWindow::Init(WindowProps^ props)
{
    ...
    WindowSizeDelegate^ windowSizeDelegate = gcnew WindowSizeDelegate(this, WindowSizeCallback);
    GCHandle handle = GCHandle::Alloc(windowSizeDelegate);
	IntPtr ptr = Marshal::GetFunctionPointerForDelegate(windowSizeDelegate);
	glfwSetWindowSizeCallback(m_window, static_cast<GLFWwindowsizefun>(ptr->ToPointer()));
}
```

## GLFW GetWindowUserPointer

The next issue I hit was that because I was not using the `glfwSetWindowUserPointer` method when creating the window object I could not use the `glfwGetWindowUserPointer` to get the data object from the window. My callback function is also static so I wouldn't be able to access the `m_data` field where I was currently storing that data.

I decided to make my callback functions member functions, so that they could access `m_data` field directly. The main reason for this was that I didn't want to have to create a native window data struct and have to marshal the data back and forth from native to managed code everytime an event was raised. It seemed like it would be better to keep as much of it in managed code as possible.

Adding protected methods to the window header file.
```c++
public ref class WindowsWindow : public IWindow
{
    ...
protected:
    virtual void OnWindowSize(GLFWwindow* window, int width, int height);
    ...
}
```

Implementing the new method and creating the delegate.
```c++
void WindowsWindow::Init(WindowProps^ props)
{
    ...
    WindowSizeDelegate^ windowSizeDelegate = gcnew WindowSizeDelegate(this, &WindowsWindow::OnWindowSize);
    GCHandle handle = GCHandle::Alloc(windowSizeDelegate);
	IntPtr ptr = Marshal::GetFunctionPointerForDelegate(windowSizeDelegate);
	glfwSetWindowSizeCallback(m_window, static_cast<GLFWwindowsizefun>(ptr->ToPointer()));
}

void WindowsWindow::OnWindowSize(GLFWwindow* window, int width, int height)
{
	m_data.Width = width;
	m_data.Height = height;

	WindowResizeEvent^ resizeEvent = gcnew WindowResizeEvent(width, height);
	m_data.EventCallback(resizeEvent);
}
```

## Reducing The Boilerplate

Obviously the 4 lines of code needed to create the delegate, GC handle, pointer and set the callback isn't too bad but definitely adds up when repeating 6 times for the various events we needed to implement. To help reduce this boilerplate code I created a macro to reduce the assignments to a single line.

```c++
#define SET_CALLBACK_DELEGATE(delegateType, memberFunction, callbackFunction, callbackType) \
{ \
	delegateType^ del = gcnew delegateType(this, &WindowsWindow::memberFunction); \
	GCHandle handle = GCHandle::Alloc(del); \
	IntPtr ptr = Marshal::GetFunctionPointerForDelegate(del); \
	callbackFunction(m_window, static_cast<callbackType>(ptr.ToPointer())); \
}

void WindowsWindow::Init(WindowProps^ props)
{
	...
	SET_CALLBACK_DELEGATE(CursorPosDelegate, OnCursorPos, glfwSetCursorPosCallback, GLFWcursorposfun)
	SET_CALLBACK_DELEGATE(KeyDelegate, OnKey, glfwSetKeyCallback, GLFWkeyfun)
	SET_CALLBACK_DELEGATE(MouseButtonDelegate, OnMouseButton, glfwSetMouseButtonCallback, GLFWmousebuttonfun)
	SET_CALLBACK_DELEGATE(ScrollDelegate, OnScroll, glfwSetScrollCallback, GLFWscrollfun)
	SET_CALLBACK_DELEGATE(WindowCloseDelegate, OnWindowClose, glfwSetWindowCloseCallback, GLFWwindowclosefun)
	SET_CALLBACK_DELEGATE(WindowSizeDelegate, OnWindowSize, glfwSetWindowSizeCallback, GLFWwindowsizefun)
    ...
}
```

## Handling Window Close

The final part of the video deals with handling the window close event and exiting the main application loop. This a very simple port of the code from C++ to C#, to modify the `OnEvent` and use the dispatcher to call a new `OnWindowClose` method on the application class.

```cs
public abstract class Application
{
    ...
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
```

## Video Link

[TheCherno - Game Engine Series - Window Events](https://www.youtube.com/watch?v=r74WxFMIEdU&list=PLlrATfBNZ98dC-V-N3m0Go4deliWHPFwT&index=12&ab_channel=TheCherno)

## Next
[Layers](https://github.com/ChrisVicary/Snowflake/blob/main/Documentation/Blog/10-Layers.md)