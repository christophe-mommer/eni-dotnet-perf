using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Data.SqlClient;
using Profi.Infra;
using Profi.Infra.Messages.Contrats;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Profi.Business.Models
{
    public class Contrat :
        IHandler<ExecuterRequeteComplexe>,
        IHandler<RecupererContrat>,
        IHandler<RecupererListeParPersonne>
    {
        public string Uid { get; set; } = "";
        public string UidTitulaire { get; set; } = "";
        public decimal Montant { get; set; }
        public DateTime Debut { get; set; }
        public string? Reduction { get; set; }
        public string? DocumentBase64 { get; set; }

        public Personne TitulaireNavigation { get; set; } = default!;

        /// <summary>
        /// Méthode simulant une requête complexe
        /// </summary>
        /// <returns>Une liste de chaînes en retour</returns>
        private static List<string> RequeteComplexe()
        {
            List<string> result = new List<string>();
            SqlConnection conn = new SqlConnection(Consts.ConnectionString);
            SqlCommand command = new SqlCommand("SELECT DISTINCT A.uid FROM CONTRAT AS A CROSS JOIN CONTRAT AS B WHERE (A.montant + B.montant < 200000) AND (A.titulaire NOT IN (SELECT titulaire FROM CONTRAT AS C WHERE (A.montant + montant < 100000) AND (B.debut < debut)))", conn);
            conn.Open();

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
        /// <param name="restrictionUIDPersonne">Code de restriction éventuel de la liste sur un identifiant d'une personne titulaire des contrats
        /// qui seront alors renvoyés par la méthode. Si le paramètre est null, on enverra tous les contrats de la base de données.</param>
        /// <returns>Une liste de contrats</returns>
        private static List<Contrat> RecupererListe(string restrictionUIDPersonne)
        {
            List<Contrat> result = new List<Contrat>();
            SqlConnection conn = new SqlConnection(Consts.ConnectionString);
            string request = "SELECT uid, titulaire, montant, debut, reduction FROM CONTRAT";
            if (!string.IsNullOrEmpty(restrictionUIDPersonne))
            {
                request += " WHERE titulaire=@titulaire";
            }

            SqlCommand command = new SqlCommand(request, conn);
            if (restrictionUIDPersonne
                != null)
                command.Parameters.Add(new SqlParameter("titulaire", restrictionUIDPersonne));
            conn.Open();
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
        /// Méthode de récupération d'un contrat en fonction de son identifiant unique
        /// </summary>
        /// <param name="UIDContrat">Identifiant du contrat à charger</param>
        /// <returns>Un objet métier de type contrat correspondant, ou null si l'identifiant ne correspondait pas à une entrée dans la base de données</returns>
        internal static Contrat Recuperer(string UIDContrat)
        {
            SqlConnection con = new SqlConnection(Consts.ConnectionString);
            SqlCommand command = new SqlCommand("SELECT uid, titulaire, montant, debut, reduction FROM CONTRAT WHERE uid=@uidcontrat", con);
            command.Parameters.Add(new SqlParameter("uidcontrat", UIDContrat));
            con.Open();
            Contrat result = null;
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
        /// <param name="Lecteur">Curseur de requête</param>
        /// <returns>Objet métier contrat créé à partir du curseur</returns>
        private static Contrat RecupererContratSurCurseur(SqlDataReader Lecteur)
        {
            Contrat result = new Contrat()
            {
                Uid = Lecteur.GetString(Lecteur.GetOrdinal("uid")),
                UidTitulaire = Lecteur.GetString(Lecteur.GetOrdinal("titulaire")),
                Montant = (int)Lecteur.GetDecimal(Lecteur.GetOrdinal("montant")),
                Debut = Lecteur.GetDateTime(Lecteur.GetOrdinal("debut")),
            };

            if (!Lecteur.IsDBNull(Lecteur.GetOrdinal("reduction")))
            {
                result.Reduction = Lecteur.GetString(Lecteur.GetOrdinal("reduction"));
            }

            // Réalisation d'une fusion de document pour mise à disposition dans l'objet métier du fichier Word contractuel
            string tempRep = ExtraireContratTypeDansRepertoire();

            string fichierFusion = FusionnerDocumentContrat(result, tempRep);
            result.DocumentBase64 = Convert.ToBase64String(File.ReadAllBytes(fichierFusion));

            File.Delete(fichierFusion);
            Directory.Delete(tempRep, true);

            return result;
        }


        /// <summary>
        /// Décompression du contrat type dans un répertoire temporaire
        /// </summary>
        /// <returns>L'adresse du répertoire créé</returns>
        private static string ExtraireContratTypeDansRepertoire()
        {
            string tempRep = Path.GetTempFileName();
            File.Delete(tempRep);
            Directory.CreateDirectory(tempRep);
            using (Stream Flux = new MemoryStream(File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "ContratType.docx"))))
            {
                new FastZip().ExtractZip(Flux, tempRep, FastZip.Overwrite.Always, null, ".*", ".*", false, true);
            }

            return tempRep;
        }

        /// <summary>
        /// Fusion du document contractuel avec les valeurs de l'objet métier contrat courant, la fusion consistant à remplacer
        /// les entrées paramétrées dans le document type par les vraies valeurs issues de la base de données pour un contrat donné
        /// </summary>
        /// <param name="Resultat">Le contrat courant</param>
        /// <param name="RepTemp">Le répertoire contenant les fichiers décompressés du fichier Word au format DOCX</param>
        /// <returns>Le fichier recompressé de retour avec les informations fusionnées</returns>
        private static string FusionnerDocumentContrat(Contrat Resultat, string RepTemp)
        {
            string docFile = Path.Combine(RepTemp, @"word\document.xml");
            XmlDocument xml = new XmlDocument();
            xml.Load(docFile);

            XmlNamespaceManager @namespace = new XmlNamespaceManager(xml.NameTable);
            @namespace.AddNamespace("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            foreach (XmlNode node in xml.SelectNodes("//w:t[contains(., '[FUSION_')]", @namespace))
            {
                node.InnerText = node.InnerText.Replace("[FUSION_uid]", Resultat.Uid);
                node.InnerText = node.InnerText.Replace("[FUSION_montant]", Resultat.Montant.ToString("C"));
                node.InnerText = node.InnerText.Replace("[FUSION_debut]", Resultat.Debut.ToLongDateString());
            }

            xml.Save(docFile);

            string fichierFusion = Path.GetTempFileName();
            new FastZip().CreateZip(fichierFusion, RepTemp, true, ".*");
            return fichierFusion;
        }

        public Task<object> HandleMessage(ExecuterRequeteComplexe message)
        {
            object result = RequeteComplexe();
            return Task.FromResult(result);
        }

        public Task<object> HandleMessage(RecupererListeParPersonne message)
        {
            object result = RecupererListe(message.PersonneId);
            return Task.FromResult(result);
        }

        public Task<object> HandleMessage(RecupererContrat message)
        {
            object result = Recuperer(message.Id);
            return Task.FromResult(result);
        }
    }
}
