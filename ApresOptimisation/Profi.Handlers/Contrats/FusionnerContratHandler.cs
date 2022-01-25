using Microsoft.Extensions.Caching.Memory;
using Profi.Business.Models;
using Profi.Data.Abstractions;
using Profi.Infra;
using Profi.Infra.Messages.Contrats;
using System.IO.Compression;
using System.Xml;

namespace Profi.Handlers.Contrats
{
    public class FusionnerContratHandler : IHandler<FusionnerContrat>
    {
        private readonly IMemoryCache cache;
        private readonly IContratRepository repo;

        public FusionnerContratHandler(
            IMemoryCache cache,
            IContratRepository repo)
        {
            this.cache = cache;
            this.repo = repo;
        }

        public async Task<object?> HandleMessage(FusionnerContrat message)
        {
            var contrat = repo.Recuperer(message.Id);
            if (contrat is null)
            {
                return Task.FromResult<object?>(null);
            }
            using var flux = new MemoryStream();
            flux.Write(await cache.GetOrCreateAsync("contrat_Type", async c =>
            {
                var data = await File.ReadAllBytesAsync(Path.Combine(AppContext.BaseDirectory, "ContratType.docx"));
                c.SetValue(data);
                return data;
            }));
            flux.Position = 0;
            using (var zip = new ZipArchive(flux, ZipArchiveMode.Update, true))
            {
                var doc = zip.Entries.FirstOrDefault(z => z.FullName == "word/document.xml");
                FusionnerDocumentContrat(contrat, doc.Open());
            }

            flux.Position = 0;
            return flux.ToArray();
        }

        /// <summary>
        /// Fusion du document contractuel avec les valeurs de l'objet métier contrat courant, la fusion consistant à remplacer
        /// les entrées paramétrées dans le document type par les vraies valeurs issues de la base de données pour un contrat donné
        /// </summary>
        /// <param name="contrat">Le contrat courant</param>
        /// <param name="sourceStream">Stream source contenant le fichier contrat type</param>
        /// <returns>Le fichier recompressé de retour avec les informations fusionnées</returns>
        private static void FusionnerDocumentContrat(Contrat contrat, Stream sourceStream)
        {
            using var readStream = new MemoryStream();
            sourceStream.CopyTo(readStream);
            readStream.Position = 0;
            sourceStream.Position = 0;

            XmlDocument xml = new XmlDocument();
            xml.Load(readStream);

            XmlNamespaceManager @namespace = new XmlNamespaceManager(xml.NameTable);
            @namespace.AddNamespace("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            foreach (XmlNode node in xml.SelectNodes("//w:t[contains(., 'FUSION_')]", @namespace))
            {
                node.InnerText = node.InnerText.Replace("FUSION_uid", contrat.Uid);
                node.InnerText = node.InnerText.Replace("FUSION_montant", contrat.Montant.ToString("C"));
                node.InnerText = node.InnerText.Replace("FUSION_debut", contrat.Debut.ToLongDateString());
            }

            xml.Save(sourceStream);
        }

    }
}
