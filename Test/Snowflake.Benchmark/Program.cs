using BenchmarkDotNet.Running;
using Snowflake.Benchmark.Logging;

namespace Snowflake.Benchmark;

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run();
    }
}