using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace Profi.Business.Models
{
    public class Personne
    {
        public string Uid { get; set; } = "";
        public string Nom { get; set; } = "";
        public string Prenom { get; set; } = "";
        public string Description { get; set; } = "";

        public List<Contrat> Contrats { get; set; } = new List<Contrat>();

        /// <summary>
        /// Méthode de récupération de la liste des personnes depuis la base de données
        /// </summary>
        /// <returns>Une liste de personnes</returns>
        public static List<Personne> RecupererListe()
        {
            List<Personne> resultat = new List<Personne>();
            SqlConnection conn = new SqlConnection(Consts.ConnectionString);
            SqlCommand command = new SqlCommand("SELECT uid, nom, prenom, description FROM PERSONNE", conn);
            conn.Open();
            using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (reader.Read())
                {
                    resultat.Add(RecupererPersonneSurCurseur(reader));
                }
            }

            return resultat;
        }

        /// <summary>
        /// Méthode de récupération d'une personne en fonction de son identifiant unique
        /// </summary>
        /// <param name="UIDPersonne">Identifiant de la personne à charger</param>
        /// <returns>Un objet métier de type personne correspondant, ou null si l'identifiant ne correspondait pas à une entrée dans la base de données</returns>
        public static Personne Recuperer(string UIDPersonne)
        {
            SqlConnection conn = new SqlConnection(Consts.ConnectionString);
            SqlCommand command = new SqlCommand("SELECT uid, nom, prenom, description FROM PERSONNE WHERE uid=@uidpersonne", conn);
            command.Parameters.Add(new SqlParameter("uidpersonne", UIDPersonne));
            conn.Open();
            Personne resultat = null;
            using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (reader.Read())
                {
                    resultat = RecupererPersonneSurCurseur(reader);
                }
            }

            // Si on a trouvé la personne, on rajoute ses contrats
            if (resultat != null)
            {
                command = new SqlCommand("SELECT uid FROM CONTRAT WHERE titulaire=@uidpersonne", conn);
                command.Parameters.Add(new SqlParameter("uidpersonne", UIDPersonne));
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        resultat.Contrats.Add(Contrat.Recuperer(reader.GetString(reader.GetOrdinal("uid"))));
                    }
                }
            }

            return resultat;
        }

        /// <summary>
        /// Fonction utilitaire de peuplement de l'objet métier personne en fonction du curseur sur une table de données
        /// </summary>
        /// <param name="Lecteur">Curseur de requête</param>
        /// <returns>Objet métier personne créé à partir du curseur</returns>
        private static Personne RecupererPersonneSurCurseur(SqlDataReader Lecteur)
        {
            return new Personne()
            {
                Uid = Lecteur.GetString(Lecteur.GetOrdinal("uid")),
                Nom = Lecteur.GetString(Lecteur.GetOrdinal("nom")),
                Prenom = Lecteur.GetString(Lecteur.GetOrdinal("prenom")),
                Description = Lecteur.GetString(Lecteur.GetOrdinal("description"))
            };
        }
    }
}
