using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Profi.Business.Data.Models;
using Profi.Business.Options;
using Profi.Business.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Profi.Business.Repositories
{
    public class ContratSqlRepository : IContratRepository
    {
        private readonly IOptions<DatabaseOptions> options;

        public ContratSqlRepository(
            IOptions<DatabaseOptions> options)
        {
            this.options = options;
        }

        /// <summary>
        /// Méthode simulant une requête complexe
        /// </summary>
        /// <returns>Une liste de chaînes en retour</returns>
        public List<string> RequeteComplexe()
        {
            List<string> result = new List<string>();
            SqlConnection connection = new SqlConnection(options.Value.ConnectionString);
            SqlCommand command = new SqlCommand("SELECT DISTINCT A.uid FROM CONTRAT AS A CROSS JOIN CONTRAT AS B WHERE (A.montant + B.montant < 200000) AND (A.titulaire NOT IN (SELECT titulaire FROM CONTRAT AS C WHERE (A.montant + montant < 100000) AND (B.debut < debut)))", connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            using (reader)
            {
                while (reader.Read())
                {
                    result.Add(reader.GetString(0));
                }
            }

            return result;
        }

        /// <summary>
        /// Méthode de récupération d'une liste de contrats depuis la base de données, potentiellement limitée à une personne titulaire
        /// </summary>
        /// <param name="restrictionUidPersonne">Code de restriction éventuel de la liste sur un identifiant d'une personne titulaire des contrats
        /// qui seront alors renvoyés par la méthode. Si le paramètre est null, on enverra tous les contrats de la base de données.</param>
        /// <returns>Une liste de contrats</returns>
        public List<Contrat> RecupererListe(string restrictionUidPersonne)
        {
            List<Contrat> result = new List<Contrat>();
            SqlConnection connection = new SqlConnection(options.Value.ConnectionString);
            string request = "SELECT uid uid, titulaire titulaire, montant montant, debut debut, reduction reduction FROM CONTRAT";
            if (!string.IsNullOrEmpty(restrictionUidPersonne))
            {
                request += " WHERE titulaire=@titulaire";
            }

            SqlCommand commande = new SqlCommand(request, connection);
            if (restrictionUidPersonne != null)
            {
                commande.Parameters.Add(new SqlParameter("titulaire", restrictionUidPersonne));
            }

            connection.Open();
            using (SqlDataReader reader = commande.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (reader.Read())
                {
                    result.Add(RecupererContratSurCurseur(reader));
                }
            }

            return result;
        }

        /// <summary>
        /// Méthode de récupération d'un contrat en fonction de son identifiant unique
        /// </summary>
        /// <param name="ContractUid">Identifiant du contrat à charger</param>
        /// <returns>Un objet métier de type contrat correspondant, ou null si l'identifiant ne correspondait pas à une entrée dans la base de données</returns>
        public Contrat? Recuperer(string ContractUid)
        {
            SqlConnection connection = new SqlConnection(options.Value.ConnectionString);
            SqlCommand command = new SqlCommand("SELECT uid uid, titulaire titulaire, montant montant, debut debut, reduction reduction FROM CONTRAT WHERE uid=@uidcontrat", connection);
            command.Parameters.Add(new SqlParameter("uidcontrat", ContractUid));
            connection.Open();
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
        /// Fonction utilitaire de peuplement de l'objet métier contrat en fonction du curseur sur une table de données
        /// </summary>
        /// <param name="reader">Curseur de requête</param>
        /// <returns>Objet métier contrat créé à partir du curseur</returns>
        private Contrat RecupererContratSurCurseur(SqlDataReader reader)
        {
            Contrat result = new Contrat()
            {
                Uid = reader.GetString(reader.GetOrdinal("uid")),
                Titulaire = reader.GetString(reader.GetOrdinal("titulaire")),
                Montant = (int)reader.GetDecimal(reader.GetOrdinal("montant")),
                Debut = reader.GetDateTime(reader.GetOrdinal("debut")),
            };

            if (!reader.IsDBNull(reader.GetOrdinal("reduction")))
            {
                result.Reduction = reader.GetString(reader.GetOrdinal("reduction"));
            }

            // Réalisation d'une fusion de document pour mise à disposition dans l'objet métier du fichier Word contractuel
            string tempRep = ExtraireContratTypeDansRepertoire();

            string fusionFile = FusionnerDocumentContrat(result, tempRep);
            result.DocumentBase64 = Convert.ToBase64String(File.ReadAllBytes(fusionFile));

            File.Delete(fusionFile);
            Directory.Delete(tempRep, true);

            return result;
        }


        /// <summary>
        /// Décompression du contrat type dans un répertoire temporaire
        /// </summary>
        /// <returns>L'adresse du répertoire créé</returns>
        private string ExtraireContratTypeDansRepertoire()
        {
            string tempRep = Path.GetTempFileName();
            File.Delete(tempRep);
            Directory.CreateDirectory(tempRep);
            using (Stream? flux = typeof(Contrat).Assembly.GetManifestResourceStream("METIER_CONTRATS.ContratType.docx"))
            {
                if (flux != null)
                {
                    new FastZip().ExtractZip(flux, tempRep, FastZip.Overwrite.Always, null, ".*", ".*", false, true);
                }
            }

            return tempRep;
        }

        /// <summary>
        /// Fusion du document contractuel avec les valeurs de l'objet métier contrat courant, la fusion consistant à remplacer
        /// les entrées paramétrées dans le document type par les vraies valeurs issues de la base de données pour un contrat donné
        /// </summary>
        /// <param name="result">Le contrat courant</param>
        /// <param name="tempRep">Le répertoire contenant les fichiers décompressés du fichier Word au format DOCX</param>
        /// <returns>Le fichier recompressé de retour avec les informations fusionnées</returns>
        private string FusionnerDocumentContrat(Contrat result, string tempRep)
        {
            string file = Path.Combine(tempRep, @"word\document.xml");
            XmlDocument dom = new XmlDocument();
            dom.Load(file);

            XmlNamespaceManager @namespace = new XmlNamespaceManager(dom.NameTable);
            @namespace.AddNamespace("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            foreach (XmlNode node in dom.SelectNodes("//w:t[contains(., '[FUSION_')]", @namespace))
            {
                node.InnerText = node.InnerText.Replace("[FUSION_uid]", result.Uid);
                node.InnerText = node.InnerText.Replace("[FUSION_montant]", result.Montant.ToString("C"));
                if (result.Debut.HasValue)
                {
                    node.InnerText = node.InnerText.Replace("[FUSION_debut]", result.Debut.Value.ToLongDateString());
                }
            }

            dom.Save(file);

            string fusionFile = Path.GetTempFileName();
            new FastZip().CreateZip(fusionFile, tempRep, true, ".*");
            return fusionFile;
        }
    }
}
