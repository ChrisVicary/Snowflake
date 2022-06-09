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

I did also omit the call to `glfwSetWindowUserPointer()` getting a `void*` from a managed type

## Video Link

[TheCherno - Game Engine Series - EventSystem](https://www.youtube.com/watch?v=sULV3aB2qeU&list=PLlrATfBNZ98dC-V-N3m0Go4deliWHPFwT&index=10&ab_channel=TheCherno)

## Next
[Premake - Part 2](https://github.com/ChrisVicary/Snowflake/blob/main/Documentation/Blog/08-PremakePart2.md)