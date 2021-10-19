using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkBoxing
{
    [MemoryDiagnoser]
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net48)]
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.CoreRt31)]
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.CoreRt50)]
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.CoreRt60)]
    public class BoxingBenchmark
    {
        private static int Total;

        [Benchmark]
        public void CallBoxed()
        {
            for (int i = 0; i < 1_000_000; i++)
            {
                FonctionBox(1);
            }
        }

        [Benchmark]
        public void CallNative()
        {
            for (int i = 0; i < 1_000_000; i++)
            {
                FonctionNative(1);
            }
        }

        private void FonctionBox(object value)
        {
            Total += (int)value;
        }
        private void FonctionNative(int value)
        {
            Total += value;
        }
    }
}
