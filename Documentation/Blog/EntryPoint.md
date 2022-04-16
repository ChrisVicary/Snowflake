# Entry Point

## The Application Class

The `Application` and `SandboxApp` classes are created in the Snowflake and Sandbox projects respectively and look fairly similar to the C++ code from the tutorials.

### Application
```
namespace Snowflake;

public abstract class Application
{
    public virtual void Run()
    {
        while (true);
    }
}
```

### SandboxApp
```
using Snowflake;

internal class SandboxApp : Application
{ 
}
```

## The Main Function

It didn't take long to hit my first major code change, in his tutorial series TheCherno uses a `main` function in the Hazel library as the entry point for the application. Unfortunately in C# I need to have a `main` function in the executable library, you will get a compile error without it. To keep things simple for now I have created a basic `Program` class with the `main` function. In the function I create a new instance of the `SandboxApp` class and call the `Run` method defined on the base `Application` class.

```
namespace Sandbox;

internal class Program
{
    static void Main(string[] args)
    {
        var application = new SandboxApp();
        application.Run();
    }
}
```