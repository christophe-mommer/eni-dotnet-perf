using ICSharpCode.SharpZipLib.Zip;
using Profi.Business.Models;
using Profi.Data.Abstractions;
using Profi.Infra;
using Profi.Infra.Messages.Contrats;
using System.Xml;

namespace Profi.Handlers.Contrats
{
    public class FusionnerContratHandler : IHandler<FusionnerContrat>
    {
        private readonly IContratRepository repo;

        public FusionnerContratHandler(
            IContratRepository repo)
        {
            this.repo = repo;
        }

        public Task<object?> HandleMessage(FusionnerContrat message)
        {
            var contrat = repo.Recuperer(message.Id);
            if (contrat is null)
            {
                return Task.FromResult<object?>(null);
            }
            var dir = ExtraireContratTypeDansRepertoire();
            object result = FusionnerDocumentContrat(contrat, dir);
            return Task.FromResult(result);
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
            var fileBytes = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "ContratType.docx"));
            using (Stream Flux = new MemoryStream(fileBytes))
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
            foreach (XmlNode node in xml.SelectNodes("//w:t[contains(., 'FUSION_')]", @namespace))
            {
                node.InnerText = node.InnerText.Replace("FUSION_uid", Resultat.Uid);
                node.InnerText = node.InnerText.Replace("FUSION_montant", Resultat.Montant.ToString("C"));
                node.InnerText = node.InnerText.Replace("FUSION_debut", Resultat.Debut.ToLongDateString());
            }

            xml.Save(docFile);

            string fichierFusion = Path.GetTempFileName();
            new FastZip().CreateZip(fichierFusion, RepTemp, true, ".*");
            return fichierFusion;
        }

    }
}
