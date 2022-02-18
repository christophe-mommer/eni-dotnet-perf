using BenchmarkDotNet.Attributes;
using System.Text;

namespace Strings
{
    [MemoryDiagnoser]
    public class StringBenchmark
    {
        [Benchmark]
        public void StringConcat()
        {
            string result = "";
            for (int i = 1; i <= 20; i++)
            {
                result += i;
                if (i != 20)
                    result += " ";
            }
        }

        [Benchmark]
        public void StringBuilder()
        {
            var sb = new StringBuilder();
            for (int i = 1; i <= 20; i++)
            {
                sb.Append(i);
                if (i != 20)
                    sb.Append(' ');
            }
            string result = sb.ToString();
        }

        [Benchmark]
        public void StringBuilder_Size()
        {
            var sb = new StringBuilder(50);
            for (int i = 1; i <= 20; i++)
            {
                sb.Append(i);
                if (i != 20)
                    sb.Append(' ');
            }
            string result = sb.ToString();
        }

    }
}
