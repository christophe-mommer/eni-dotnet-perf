using System;
using System.Windows.Forms;

namespace FuiteMemoire
{
    /// <summary>
    /// La feuille principale sert uniquement à lancer des fenêtres secondaires,
    /// ce n'est pas elle qui fuit de la mémoire
    /// </summary>
    public partial class FeuillePrincipale : Form
    {
        public FeuillePrincipale()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // On simule cinq appels d'une fenêtre
//            for (int Index = 0; Index < 5; Index++)
//            {
                // On instancie la fenêtre et on l'affiche, elle se ferme d'elle-même
                FeuilleSecondaire Feuille = new FeuilleSecondaire();
                Feuille.ShowDialog();

                // La bonne pratique consistant à appeler Dispose sur la fenêtre
                // ne changera rien à la fuite mémoire dans la feuille secondaire
                Feuille.Dispose();
//            }

            // Il est essentiel d'appeler le ramasse-miettes pour être sûr
            // que la mémoire est bien perdue (fuite) et non pas seulement
            // qu'elle n'est pas encore recyclée mais le sera plus tard
            GC.Collect(GC.MaxGeneration);
        }
    }
}
