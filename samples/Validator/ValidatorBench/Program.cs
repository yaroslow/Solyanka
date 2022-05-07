using BenchmarkDotNet.Running;

namespace ValidatorBench;

/// <summary>
/// Main program class
/// </summary>
public static class Program
{
    /// <summary>
    /// Entry point
    /// </summary>
    public static void Main()
    {
        BenchmarkRunner.Run<ValidationBenchmark>();
    }
}
