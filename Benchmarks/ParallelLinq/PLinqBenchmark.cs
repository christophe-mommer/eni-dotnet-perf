using BenchmarkDotNet.Attributes;
using System.Collections.Concurrent;

namespace ParallelLinq
{
    public class PLinqBenchmark
    {
        private List<int> data;

        [GlobalSetup]
        public void Setup()
        {
            data = Enumerable.Range(1, 50_000).ToList();
        }

        [Benchmark]
        public void Normal_Linq()
        {
            var result = new List<int>();
            foreach (var item in data)
            {
                if (EstPremier(item))
                {
                    result.Add(item);
                }
            }
        }

        [Benchmark]
        public void AsParallel_Linq()
        {
            var result = new ConcurrentBag<int>();
            foreach (var item in data.AsParallel())
            {
                if (EstPremier(item))
                {
                    result.Add(item);
                }
            }
        }

        [Benchmark]
        public void Parallel_Linq()
        {
            var result = new ConcurrentBag<int>();
            Parallel.ForEach(data, item =>
            {
                if (EstPremier(item))
                {
                    result.Add(item);
                }
            });
        }

        private static bool EstPremier(int Nombre)
        {
            for (int ind = 2; ind < Nombre - 1; ind++)
            {
                if (Nombre % ind == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
