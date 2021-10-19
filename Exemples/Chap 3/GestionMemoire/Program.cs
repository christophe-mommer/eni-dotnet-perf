using GestionMemoire;

decimal m = 100m;
int pourcent = 10;

AppliquerReduction(m, ref pourcent);

var c = new Coordonnee(10, 10);
c.X = 20;

void AppliquerReduction(decimal montant, ref int pourcent)
{
    var newMontant = montant - (montant / 100 * pourcent);
    System.Console.WriteLine(newMontant);
}