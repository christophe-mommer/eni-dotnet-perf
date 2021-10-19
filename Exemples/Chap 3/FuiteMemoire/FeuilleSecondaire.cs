using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Microsoft.Win32;

namespace FuiteMemoire
{
    /// <summary>
    /// La feuille secondaire est celle qui provoque la fuite de mémoire
    /// </summary>
    public partial class FeuilleSecondaire : Form
    {
        List<decimal> Liste = new List<decimal>();

        public FeuilleSecondaire()
        {
            // On initialise la feuille et on remplit une liste de nombres juste pour augmenter la mémoire
            // utilisée par cette feuille, de façon à mieux voir les montées en mémoire dans le moniteur
            InitializeComponent();
            for (int Index = 0; Index < 100000; Index++)
                Liste.Add(0M);

            // La fuite mémoire vient du fait qu'en s'abonnant à un abonnement système sans s'en désabonner,
            // la présente fenêtre rend son recyclage par le ramasse-miette impossible
            SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
        }

        void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            // Ce qu'on fait ici n'a que peu d'importance
        }

        private void FeuilleSecondaire_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Si on souhaite régler le problème de fuite mémoire démontré ici, il suffit de décommenter
            // la ligne suivante : plus rien ne retiendra la fenêtre une fois close d'être recyclée
            //SystemEvents.PowerModeChanged -= new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
        }
    }
}
