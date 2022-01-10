using Microsoft.Data.SqlClient;
using Profi.Business.Models;
using Profi.Data.Abstractions;
using System.Data;

namespace Profi.Data.Implementations
{
    public class SqlPersonneRepository : IPersonneRepository
    {
        private readonly IContratRepository contratRepo;

        public SqlPersonneRepository(
            IContratRepository contratRepo)
        {
            this.contratRepo = contratRepo;
        }

        public Personne? Recuperer(string UIDPersonne)
        {
            using SqlConnection conn = new SqlConnection(Consts.ConnectionString);
            SqlCommand command = new SqlCommand("SELECT uid, nom, prenom, description FROM PERSONNE WHERE uid=@uidpersonne", conn);
            command.Parameters.Add(new SqlParameter("uidpersonne", UIDPersonne));
            conn.Open();
            Personne? resultat = null;
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
                SqlDataReader reader2 = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader2.Read())
                {
                    var contrat = contratRepo.Recuperer(reader2.GetString(reader2.GetOrdinal("uid")));
                    if (contrat is not null)
                    {
                        resultat.Contrats.Add(contrat);
                    }
                }
            }

            return resultat;
        }

        public List<Personne> RecupererListe()
        {
            List<Personne> resultat = new List<Personne>();
            using (SqlConnection conn = new SqlConnection(Consts.ConnectionString))
            {
                using SqlCommand command = new SqlCommand("SELECT uid, nom, prenom, description FROM PERSONNE", conn);
                conn.Open();
                using SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    resultat.Add(RecupererPersonneSurCurseur(reader));
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
