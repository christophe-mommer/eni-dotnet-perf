using BenchmarkDotNet.Running;
namespace BenchmarkBoxing
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var summary = BenchmarkRunner.Run<BoxingBenchmark>();
		}
	}
}