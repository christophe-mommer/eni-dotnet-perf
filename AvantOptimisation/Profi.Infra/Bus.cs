using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profi.Infra
{
    public class Bus
    {
        /// <summary>
        /// Stockage du bus courant (et unique) de l'application
        /// </summary>
        public static Bus Current = null;

        /// <summary>
        /// Liste des contrôleurs qui ont été chargés par le bus et rattachés à lui-même
        /// </summary>
        private List<IControleur> controllers = new List<IControleur>();

        /// <summary>
        /// Variable servant à stocker temporairement des objets le temps d'un message, vu que seules des chaînes peuvent être véhiculées
        /// dans les paramètres du message lui-même, et qu'on a parfois besoin de passer un objet complexe, typiquement un contrôle pour affichage
        /// </summary>
        private object solt;

        /// <summary>
        /// Le constructeur ne sert qu'à garder l'instance
        /// </summary>
        public Bus()
        {
            Current = this;
        }

        /// <summary>
        /// Permet de garder un objet en mémoire pour passage à un autre contrôleur sans avoir à sérialiser le contenu dans un message
        /// </summary>
        /// <param name="Contenu">L'objet à positionner dans le buffer</param>
        public void Stocker(object Contenu)
        {
            solt = Contenu;
        }

        /// <summary>
        /// Fonction inverse de la précédente, permettant de retrouver l'objet depuis le buffer
        /// </summary>
        /// <returns>L'objet qui se trouve dans le buffer, en attente de consommation</returns>
        public object LireObjetStocke()
        {
            return solt;
        }

        /// <summary>
        /// Fonction d'ajout d'un contrôleur sur le bus
        /// </summary>
        /// <param name="Controleur">Instance de contrôleur à prendre en compte</param>
        public void AjouterControleur(IControleur Controleur)
        {
            controllers.Add(Controleur);
        }

        /// <summary>
        /// Fonction principale du bus, à savoir dispatch d'un message qu'on lui passe à tous les contrôleurs, ces derniers étant ensuite
        /// libres de traiter ou pas ce message
        /// </summary>
        /// <param name="Message">Message à dispatcher</param>
        public void EnvoyerMessage(Message Message)
        {
            foreach (IControleur Controleur in controllers)
            {
                Controleur.GererMessage(Message);
            }
        }
    }
}
