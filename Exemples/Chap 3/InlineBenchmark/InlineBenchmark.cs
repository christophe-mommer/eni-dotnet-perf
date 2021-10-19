using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InlineBenchmark
{
    [MemoryDiagnoser]
    //[SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net48)]
    //[SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.CoreRt)]
    public class InlineBenchmark
    {
        [Benchmark]
        public void LoopWithoutInlining()
        {
            for (int i = 0; i < 1_000_000; i++)
            {
                FonctionGlobaleWithoutInlining(1);
            }
        }

        private int FonctionGlobaleWithoutInlining(int Parametre)
        {
            int Resultat = FonctionWithoutInlining(Parametre);
            return Resultat + 1;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private int FonctionWithoutInlining(int Parametre)
        {
            return Parametre * 2;
        }

        [Benchmark]
        public void LoopWithInlining()
        {
            for (int i = 0; i < 1_000_000; i++)
            {
                FonctionGlobaleWithInlining(1);
            }
        }

        private int FonctionGlobaleWithInlining(int Parametre)
        {
            int Resultat = FonctionWithInlining(Parametre);
            return Resultat + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int FonctionWithInlining(int Parametre)
        {
            return Parametre * 2;
        }
    }
}
