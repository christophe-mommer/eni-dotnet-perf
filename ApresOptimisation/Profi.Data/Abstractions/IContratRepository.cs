using Profi.Business.Models;

namespace Profi.Data.Abstractions
{
    public interface IContratRepository
    {
        /// <summary>
        /// Méthode de récupération d'une liste de contrats depuis la base de données, potentiellement limitée à une personne titulaire
        /// </summary>
        /// <param name="restrictionUIDPersonne">Code de restriction éventuel de la liste sur un identifiant d'une personne titulaire des contrats
        /// qui seront alors renvoyés par la méthode. Si le paramètre est null, on enverra tous les contrats de la base de données.</param>
        /// <returns>Une liste de contrats</returns>
        List<Contrat> RecupererListe(string restrictionUIDPersonne);

        /// <summary>
        /// Méthode de récupération d'un contrat en fonction de son identifiant unique
        /// </summary>
        /// <param name="UIDContrat">Identifiant du contrat à charger</param>
        /// <returns>Un objet métier de type contrat correspondant, ou null si l'identifiant ne correspondait pas à une entrée dans la base de données</returns>
        Contrat? Recuperer(string UIDContrat);
    }
}
