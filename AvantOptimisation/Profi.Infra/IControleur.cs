using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profi.Infra
{
    public interface IControleur
    {
        /// <summary>
        /// Fonction qui sera appelée pour initialiser le contrôleur
        /// </summary>
        /// <returns>true si l'initialisation s'est correctement déroulée</returns>
        bool Initialiser();

        /// <summary>
        /// Fonction principale des contrôleurs, consistant à traiter (ou laisser passer) les messages envoyés par le bus
        /// </summary>
        /// <param name="Message">Message en provenance du bus</param>
        void GererMessage(Message Message);
    }
}
