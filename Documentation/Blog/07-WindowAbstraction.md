# Window Abstraction

## Window Interface

My plan for all of the OpenGL or platform specific code is to have an interface in C# to define the abstract and then an implementation somewhere else, likely C++/CLI, specific to the platform. My C# `IWindow` interface is very similar to the pure virtual `Window` class in Hazel. The main difference it using properties for width and height and not creating a static create function. I also decided not to create an alias for the event callback, mainly because I would need to recreate the alias everywhere I needed it.

### IWindow Interface
```cs
public interface IWindow
{
    uint Width { get; }
    uint Height { get; }

    void OnUpdate();

    void SetEventCallback(Action<Event> callback);
    void SetVSync(bool enabled);
    bool IsVSync();
}
```

## Window Implementation

For the implementation I created a new C++/CLI Class Library project targetting .Net 6. This creates a project with a bunch of existing items, include pch files as well as some resources. I added a WindowsWindow.h and WindowsWindow.cpp files, these very closely resemble the C++ implementations in Hazel. 

When creating the GLFW window using the `glfwCreateWindow()` method I needed to pass a `const char*` as the title of the window. So I needed to convert my current .Net `System.String` to a `const char*`. This was the first bit of managed to native marshalling that I have had to do thus far. Thankfully there are some marshalling functions built in to the .Net libraries. So to manage this conversion I just needed a marshalling context:

```c++
msclr::interop::marshal_context^ context = gcnew msclr::interop::marshal_context();
const char* title = context->marshal_as<const char*>(m_data.Title);
m_window = glfwCreateWindow((int)props->Width, (int)props->Height, title, nullptr, nullptr);
```

I did also omit the call to `glfwSetWindowUserPointer()` getting a `void*` from a managed type didn't seem possible. I'm not sure how necessary this is and might need to revisit it later.

## GLFW

When adding GLFW to the project I ended up adding some CMake files to get everything working. This is making me re-think my decision to skip premake. It would be nice for everything to use the same build system.

## Creating the Window

To show the window in the Sandbox I needed to have a way for the application to construct an `IWindow` implementation, TheCherno uses a static abstract method for this, interestingly this is a new feature added to C# 10. I could do something very similar, however I decided to stick to the dependency injection root for now. The `Application` class has been modified to take a `Func<IWindow>` as a constructor parameter, this will be the factory method that the application used to construct a window.

In the Sandbox project I added a reference to the new Snowflake.Windows project, then added a singleton factory function to create the Window. The `CreateApplication` method the passes the factory function to the application constructor. The base application class then uses the factory function to create the window before starting the main game loop.

### Sandbox Bootstrapper
```cs
internal class SandboxBootstrapper : Bootstrapper<SandboxApp>
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);
        services.AddSingleton<Func<IWindow>>(() => new WindowsWindow(new WindowProps()));
    }

    protected override SandboxApp CreateApplication(IServiceProvider serviceProvider) 
        => new SandboxApp(
            serviceProvider.GetRequiredService<ILogger<Application>>(), 
            serviceProvider.GetRequiredService<Func<IWindow>>());
}
```

## Video Link

[TheCherno - Game Engine Series - Window Abstraction and GLFW](https://www.youtube.com/watch?v=sULV3aB2qeU&list=PLlrATfBNZ98dC-V-N3m0Go4deliWHPFwT&index=11&ab_channel=TheCherno)

## Next
[Premake - Part 2](https://github.com/ChrisVicary/Snowflake/blob/main/Documentation/Blog/08-PremakePart2.md)