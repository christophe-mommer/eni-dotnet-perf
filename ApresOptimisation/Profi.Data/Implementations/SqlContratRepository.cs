using Microsoft.Data.SqlClient;
using Profi.Business.Models;
using Profi.Data.Abstractions;
using System.Data;

namespace Profi.Data.Implementations
{
    public class SqlContratRepository : IContratRepository
    {
        /// <summary>
        /// Méthode de récupération d'un contrat en fonction de son identifiant unique
        /// </summary>
        /// <param name="UIDContrat">Identifiant du contrat à charger</param>
        /// <returns>Un objet métier de type contrat correspondant, ou null si l'identifiant ne correspondait pas à une entrée dans la base de données</returns>
        public Contrat? Recuperer(string UIDContrat)
        {
            using var connection = new SqlConnection(Consts.ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT uid, titulaire, montant, debut, reduction FROM CONTRAT WHERE uid=@uidcontrat", connection);
            command.Parameters.Add(new SqlParameter("uidcontrat", UIDContrat));
            Contrat? result = null;
            using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (reader.Read())
                {
                    result = RecupererContratSurCurseur(reader);
                }
            }

            return result;
        }

        /// <summary>
        /// Méthode de récupération d'une liste de contrats depuis la base de données, potentiellement limitée à une personne titulaire
        /// </summary>
        /// <param name="restrictionUIDPersonne">Code de restriction éventuel de la liste sur un identifiant d'une personne titulaire des contrats
        /// qui seront alors renvoyés par la méthode. Si le paramètre est null, on enverra tous les contrats de la base de données.</param>
        /// <returns>Une liste de contrats</returns>
        public List<Contrat> RecupererListe(string restrictionUIDPersonne)
        {
            List<Contrat> result = new List<Contrat>();
            string request = "SELECT uid, titulaire, montant, debut, reduction FROM CONTRAT";
            if (!string.IsNullOrEmpty(restrictionUIDPersonne))
            {
                request += " WHERE titulaire=@titulaire";
            }
            using var connection = new SqlConnection(Consts.ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(request, connection);
            if (restrictionUIDPersonne != null)
            {
                command.Parameters.Add(new SqlParameter("titulaire", restrictionUIDPersonne));
            }
            using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (reader.Read())
                {
                    result.Add(RecupererContratSurCurseur(reader));
                }
            }

            return result;
        }

        /// <summary>
        /// Fonction utilitaire de peuplement de l'objet métier contrat en fonction du curseur sur une table de données
        /// </summary>
        /// <param name="reader">Curseur de requête</param>
        /// <returns>Objet métier contrat créé à partir du curseur</returns>
        private static Contrat RecupererContratSurCurseur(SqlDataReader reader)
        {
            Contrat result = new()
            {
                Uid = reader.GetString(reader.GetOrdinal("uid")),
                UidTitulaire = reader.GetString(reader.GetOrdinal("titulaire")),
                Montant = (int)reader.GetDecimal(reader.GetOrdinal("montant")),
                Debut = reader.GetDateTime(reader.GetOrdinal("debut")),
            };

            if (!reader.IsDBNull(reader.GetOrdinal("reduction")))
            {
                result.Reduction = reader.GetString(reader.GetOrdinal("reduction"));
            }

            return result;
        }

    }
}
