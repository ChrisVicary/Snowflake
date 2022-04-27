namespace Sandbox;

internal class Program
{
    static void Main(string[] args)
    {
        var bootstrapper = new SandboxBootstrapper();
        bootstrapper.Run();
    }
}
