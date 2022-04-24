# Logging

## Logging Libraries

As with TheCherno's series I too will use an existing library to facilitate logging in my engine. There are quite few logging libraries for .Net and I've used a few different ones in the past, but for simplicity I will use the [Microsoft.Extensions.Logging](https://www.nuget.org/packages/Microsoft.Extensions.Logging/) package.

## Abstractions

The next thing that series talks about is abstractions, creating a Hazel log API so that the logging implementation could be changed at a later date without client code having to change everywhere. In general I agree with this prinicpal and I will do this a lot myself later I'm sure, but for logging specifically Microsoft has already created an abstraction in .Net with the [ILogger](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.logging.ilogger) and [ILogger\<T>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.logging.ilogger-1) interfaces. These interfaces are part of the nuget package I mentioned earlier, however they implemented in many other libraries should I decide to switch at a later date.

If I wanted to follow the example from the video I would probably setup something similar to this:
```cs
public static class Log
{
    public static ILogger CoreLogger { get; }
    public static ILogger ClientLogger { get; }

    public static void CoreTrace(string message, params object[] args) => CoreLogger.LogTrace(message, args);
    public static void CoreInformation(string message, params object[] args) => CoreLogger.LogInformation(message, args);
    public static void CoreWarning(string message, params object[] args) => CoreLogger.LogWarning(message, args);
    public static void CoreError(Exception exception, string message, params object[] args) => CoreLogger.LogError(exception, message, args);

    public static void ClientTrace(string message, params object[] args) => ClientLogger.LogTrace(message, args);
    public static void ClientInformation(string message, params object[] args) => ClientLogger.LogInformation(message, args);
    public static void ClientWarning(string message, params object[] args) => ClientLogger.LogWarning(message, args);
    public static void ClientError(Exception exception, string message, params object[] args) => ClientLogger.LogError(exception, message, args);
}
```
However I prefer not to use statics for logging, there is a place for statics and I would normally only use statics for pure functions that don't hold any state. For logging you could argue that it's just funciton, but sometimes you might want to unit test that you're sending log messages correctly and this gets trickier if you have a static logger.

## Dependency Injection

In C# we often use an IoC (Inversion of Control) container and dependency injection to provide these services to objects that need them. As with the logging there are lots of 3rd party libraries for dependency injection systems, but there is also a fairly simple one provided by another Microsoft extensions library, [Microsoft.Extensions.DependencyInjection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection/)), which I will use initially. As with the logging this might get changed later if we need more complex scenarios, but I should be able to continue to use the same abstractions.

In most cases we would have the executable create and configure the container with the services required by the application. However we don't want the client code to have to do this everytime so I will create an abstract Bootstrapper class that will setup all of the engine dependencies, like logging for example. The client code will then create a derived Bootstrapper that can override the engine configuration if desired, or just add some extra dependencies for the current application.

The basic Bootstrapper class will look like this:
```cs
public abstract class Bootstrapper<T> where T : Application
{
    public virtual void Run()
    {
        IServiceCollection services = new ServiceCollection();
        ConfigureServices(services);
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        StartApplication(serviceProvider.GetRequiredService<T>());
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(CreateApplication);
    }

    protected abstract T CreateApplication(IServiceProvider serviceProvider);

    protected virtual void StartApplication(T application) => application.Run();
}
```
This abstract Boostrapper class creates a ServiceCollection which will hold the dependencies for the whole application. It then configures the services for the application, initially this is just an abstract factory method that is used to create the client application. Finally the StartApplication method is called which creates and runs the application.

The client code will need to implement the abstract bootstrapper and the CreateApplication method to return the SandboxApp:
```cs
internal class SandboxBootstrapper : Bootstrapper<SandboxApp>
{
    protected override SandboxApp CreateApplication(IServiceProvider serviceProvider) 
        => new SandboxApp();
}
```

## Configure Logging

Now that the Dependency Injection system is setup we can add and configure the logging library. After adding the [Microsoft.Extension.Logging](https://www.nuget.org/packages/Microsoft.Extensions.Logging/) package from nuget, I have also added the Console implementation [Microsoft.Extensions.Logging.Console](https://www.nuget.org/packages/Microsoft.Extensions.Logging.Console/) package. This gives us a basic console logging implementation which will be good enough for us to start with. In the Bootstrapper class we can modify the ConfigureServices method to configure the logging.
```cs
protected virtual void ConfigureServices(IServiceCollection services)
{
    services.AddLogging(builder => 
        builder.AddSimpleConsole(options =>
        {
            options.SingleLine = true;
            options.TimestampFormat = "hh:mm:ss - ";
        })
        .SetMinimumLevel(LogLevel.Trace))
        .AddSingleton(CreateApplication);
}
```

## Consuming the Logger

With the logger setup we can use the dependency injection system to resolve the ILogger\<T> interface and pass a the implementation into the constructor for the Application. A member variable is used to hold on to the logger so that we can call the log methods in the Run method.
```cs
public abstract class Application
{
    private readonly ILogger<Application> m_logger;

    public Application(ILogger<Application> logger)
    {
        m_logger = logger;
    }

    public virtual void Run()
    {
        m_logger.LogInformation("Information from snowflake!");
        m_logger.LogWarning("Warning from snowflake!");
        m_logger.LogError("Error from snowflake!");
        while (true);
    }
}
```
In the sandbox project we do also need to modify both the SandboxApp and SandboxBootstrapper classes to resolve and pass along the logger implementation from the service provider. This is less than ideal, we could potentially have created a property/method on the base Application class to initialze the logging. The downside of that is you then need to check for null everytime you use it. With the projects setup to use nullable reference types, by requiring an ILogger\<T> implementation in the constructor we know that it can never be null.
```cs
internal class SandboxBootstrapper : Bootstrapper<SandboxApp>
{
    protected override SandboxApp CreateApplication(IServiceProvider serviceProvider) 
        => new SandboxApp(serviceProvider.GetRequiredService<ILogger<Application>>());
}

internal class SandboxApp : Application
{
    public SandboxApp(ILogger<Application> logger)
        : base(logger) { }
}
```
> To get a logger for the SandboxApp class we would need to pass a second ILogger\<T> implementation, ILogger\<SandboxApp>, into the constructor.

The console output should now look something like this:

![Logging Output](https://github.com/ChrisVicary/Snowflake/blob/main/Documentation/Blog/Images/04-Logging-01.png "Logging Output")

## Benchmarks

When it comes to performance, managed languages like C# often get a bad rep. While I'm not suggesting that this engine will be more performant that a C++ version, I do want to make sure that I take performance seriously from the start. As I add features to the engine I want to add benchmarks that look at performance, both in terms of speed and memory usage. In this case, logging is something that should be able to run with as little overhead as possible.

To test the performance of the various logging techniques I followed an excellent video from Nick Chapsas on YouTube, [High-performance logging in .NET, the poper way](https://www.youtube.com/watch?v=a26zu-pyEyg&ab_channel=NickChapsas).

In the root folder of the Snowflake solution I've added a new folder for Test code, this will contain the benchmarks as well as unit tests and system tests in the future. Inside this folder I've created a new console application and added a reference to the awesome [BenchmarkDotNet](https://www.nuget.org/packages/BenchmarkDotNet) nuget package. I'm not going to cover the code used to run the benchmarks as it's basically copied from Nick's video. But here are the results:

![Benchmark Results](https://github.com/ChrisVicary/Snowflake/blob/main/Documentation/Blog/Images/04-Logging-02.png "Benchmark Results")

This shows that the most efficent way to log using the ILogger\<T> interface is using a Source Generated extension method with a message definition. Not only is it efficient in terms of speed but because the parameters are not boxed into an object array, there isn't any heap allocation or garbage collection associated with the logging. Going forward I will endeavor to use this technique throughout the project.

## Food for Thought

There is certainly some extra boilerplate code in this C# implementation of logging than in C++, however at this point we do have a more flexible solution. In the C++ implementation if a greater level of granularity was needed for the log categories, maybe rendering, then the code would need to be modified to allow for that additional category. In the C# case the rendering code would just need to have an ILogger\<Renderer> injected into the constructor, nothing else would need to change.

## Video Link

[TheCherno - Game Engine Series - Logging](https://www.youtube.com/watch?v=meARMOmTLgE&list=PLlrATfBNZ98dC-V-N3m0Go4deliWHPFwT&index=6&ab_channel=TheCherno)