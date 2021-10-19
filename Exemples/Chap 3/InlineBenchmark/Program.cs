using BenchmarkDotNet.Running;

namespace InlineBenchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run<InlineBenchmark>();
            var b = new InlineBenchmark();

            b.LoopWithoutInlining();

            b.LoopWithInlining();
            
        }
    }
}