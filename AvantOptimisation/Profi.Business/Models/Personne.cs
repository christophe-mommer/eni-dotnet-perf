using Microsoft.Data.SqlClient;
using Profi.Infra;
using Profi.Infra.Messages.Personnes;
using System.Data;

namespace Profi.Business.Models
{
    public class Personne :
        IHandler<RecupererPersonne>,
        IHandler<RecupererPersonnes>
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
        private static List<Personne> RecupererListe()
        {
            List<Personne> resultat = new List<Personne>();
            SqlConnection conn = new SqlConnection(Consts.ConnectionString);
            SqlCommand command = new SqlCommand("SELECT uid, nom, prenom, description FROM PERSONNE", conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                resultat.Add(RecupererPersonneSurCurseur(reader));
            }


            return resultat;
        }

        /// <summary>
        /// Méthode de récupération d'une personne en fonction de son identifiant unique
        /// </summary>
        /// <param name="UIDPersonne">Identifiant de la personne à charger</param>
        /// <returns>Un objet métier de type personne correspondant, ou null si l'identifiant ne correspondait pas à une entrée dans la base de données</returns>
        private static Personne Recuperer(string UIDPersonne)
        {
            SqlConnection conn = new SqlConnection(Consts.ConnectionString);
            SqlCommand command = new SqlCommand("SELECT uid, nom, prenom, description FROM PERSONNE WHERE uid=@uidpersonne", conn);
            command.Parameters.Add(new SqlParameter("uidpersonne", UIDPersonne));
            conn.Open();
            Personne resultat = null;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            if (reader.Read())
            {
                resultat = RecupererPersonneSurCurseur(reader);
            }

            // Si on a trouvé la personne, on rajoute ses contrats
            if (resultat != null)
            {
                SqlConnection conn2 = new SqlConnection(Consts.ConnectionString);
                command = new SqlCommand("SELECT uid FROM CONTRAT WHERE titulaire=@uidpersonne", conn2);
                command.Parameters.Add(new SqlParameter("uidpersonne", UIDPersonne));
                conn2.Open();
                SqlDataReader reader2 = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader2.Read())
                {
                    resultat.Contrats.Add(Contrat.Recuperer(reader.GetString(reader2.GetOrdinal("uid"))));
                }
            }

            return resultat;
        }

        /// <summary>
        /// Fonction utilitaire de peuplement de l'objet métier personne en fonction du curseur sur une table de données
        /// </summary>
        /// <param name="reader">Curseur de requête</param>
        /// <returns>Objet métier personne créé à partir du curseur</returns>
        private static Personne RecupererPersonneSurCurseur(SqlDataReader reader)
        {
            return new Personne()
            {
                Uid = reader.GetString(reader.GetOrdinal("uid")),
                Nom = reader.GetString(reader.GetOrdinal("nom")),
                Prenom = reader.GetString(reader.GetOrdinal("prenom")),
                Description = reader.GetString(reader.GetOrdinal("description"))
            };
        }

        public Task<object> HandleMessage(RecupererPersonnes message)
        {
            object result = RecupererListe();
            return Task.FromResult(result);
        }

        public Task<object> HandleMessage(RecupererPersonne message)
        {
            object result = Recuperer(message.Id);
            return Task.FromResult(result);
        }
    }
}
