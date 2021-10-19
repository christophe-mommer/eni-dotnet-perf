using System.Runtime.CompilerServices;

namespace FonctionInline
{
    class Program
    {
        static void Main(string[] args)
        {
            int resultat = Fonction1(1);
            resultat = Fonction2(1);
        }

        private static int Fonction1(int Parametre)
        {
            int Resultat = Parametre * 2;
            return Resultat + 1;
        }

        private static int Fonction2(int Parametre)
        {
            int Resultat = FonctionIntermediaire(Parametre);
            return Resultat + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int FonctionIntermediaire(int Parametre)
        {
            return Parametre * 2;
        }
    }
}
