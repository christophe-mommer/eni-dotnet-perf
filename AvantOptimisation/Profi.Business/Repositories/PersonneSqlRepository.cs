using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Profi.Business.Data.Models;
using Profi.Business.Options;
using Profi.Business.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profi.Business.Repositories
{
    public class PersonneSqlRepository : IPersonneRepository
    {
        private readonly IContratRepository contratRepository;
        private readonly IOptions<DatabaseOptions> options;

        public PersonneSqlRepository(
            IContratRepository contratRepository,
            IOptions<DatabaseOptions> options)
        {
            this.contratRepository = contratRepository;
            this.options = options;
        }

        /// <summary>
        /// Méthode de récupération de la liste des personnes depuis la base de données
        /// </summary>
        /// <returns>Une liste de personnes</returns>
        public List<Personne> RecupererListe()
        {
            List<Personne> result = new List<Personne>();
            SqlConnection connection = new SqlConnection(options.Value.ConnectionString);
            SqlCommand command = new SqlCommand("SELECT uid uid, nom nom, prenom prenom, description description FROM PERSONNE", connection);
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (reader.Read())
                {
                    result.Add(RecupererPersonneSurCurseur(reader));
                }
            }

            return result;
        }

        /// <summary>
        /// Méthode de récupération d'une personne en fonction de son identifiant unique
        /// </summary>
        /// <param name="UIDPersonne">Identifiant de la personne à charger</param>
        /// <returns>Un objet métier de type personne correspondant, ou null si l'identifiant ne correspondait pas à une entrée dans la base de données</returns>
        public Personne? Recuperer(string UIDPersonne)
        {
            SqlConnection connection = new SqlConnection(options.Value.ConnectionString);
            SqlCommand command = new SqlCommand("SELECT uid uid, nom nom, prenom prenom, description description FROM PERSONNE WHERE uid=@uidpersonne", connection);
            command.Parameters.Add(new SqlParameter("uidpersonne", UIDPersonne));
            connection.Open();
            Personne? result = null;
            using (SqlDataReader Lecteur = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                if (Lecteur.Read())
                {
                    result = RecupererPersonneSurCurseur(Lecteur);
                }
            }

            if (result != null)
            {
                command = new SqlCommand("SELECT uid uid FROM CONTRAT WHERE titulaire=@uidpersonne", connection);
                command.Parameters.Add(new SqlParameter("uidpersonne", UIDPersonne));
                connection.Open();
                using (SqlDataReader Lecteur = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (Lecteur.Read())
                    {
                        var contrat = contratRepository.Recuperer(Lecteur.GetString(Lecteur.GetOrdinal("uid")));
                        if (contrat != null)
                        {
                            result.Contrats.Add(contrat);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Fonction utilitaire de peuplement de l'objet métier personne en fonction du curseur sur une table de données
        /// </summary>
        /// <param name="Lecteur">Curseur de requête</param>
        /// <returns>Objet métier personne créé à partir du curseur</returns>
        private Personne RecupererPersonneSurCurseur(SqlDataReader Lecteur)
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
