using BenchmarkDotNet.Attributes;

namespace ListAddRange
{
    public class ListeBenchmark
    {
        private List<string> data = new();

        [GlobalSetup]
        public void Setup()
        {
            for (int i = 0; i < 10_000; i++)
            {
                data.Add($"Numéro {i}");
            }
        }

        [Benchmark]
        public void Add()
        {
            var list = new List<string>();
            foreach (var item in data)
            {
                list.Add(item);
            }
        }

        [Benchmark]
        public void AddRange()
        {
            var list = new List<string>();
            list.AddRange(data);
        }

        [Benchmark]
        public void Add_WithSize()
        {
            var list = new List<string>(10_000);
            foreach (var item in data)
            {
                list.Add(item);
            }
        }

        [Benchmark]
        public void AddRange_WithSize()
        {
            var list = new List<string>(10_000);
            list.AddRange(data);
        }
    }
}
