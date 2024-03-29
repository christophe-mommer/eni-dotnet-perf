﻿using BenchmarkDotNet.Attributes;
using ICSharpCode.SharpZipLib.Zip;
using System.IO.Compression;
using System.Xml;

namespace ZipLibBench
{
    [MemoryDiagnoser()]
    public class Benchmark
    {
        private byte[] contratData;

        [GlobalSetup]
        public void Setup()
        {
            contratData = File.ReadAllBytes("./ContratType.docx");
        }

        [IterationSetup(Target = nameof(SharpZipLib))]
        public void IterationSetup()
        {
            if (Directory.Exists("tmp"))
                Directory.Delete("tmp", true);
            Directory.CreateDirectory("tmp");
            if (Directory.Exists("results"))
                Directory.Delete("results", true);
            Directory.CreateDirectory("results");
        }

        [Benchmark]
        public async Task SystemIOCompression()
        {
            using var flux = new MemoryStream();
            flux.Write(contratData);
            flux.Position = 0;
            using (var zip = new ZipArchive(flux, ZipArchiveMode.Update, true))
            {
                var doc = zip.Entries.FirstOrDefault(z => z.FullName == "word/document.xml");
                FusionnerDocumentContrat(doc.Open());
            }
        }

        [Benchmark]
        public void SharpZipLib()
        {
            using Stream flux = new MemoryStream(contratData);
            new FastZip().ExtractZip(flux, "tmp", FastZip.Overwrite.Always, null, ".*", ".*", false, true);
            using (var fileStream = new FileStream(Path.Combine("tmp", @"word\document.xml"), FileMode.Open))
            {
                FusionnerDocumentContrat(fileStream);
            }
            new FastZip().CreateZip("results/result.docx", "tmp", true, ".*");
        }

        private static void FusionnerDocumentContrat(Stream sourceStream)
        {
            sourceStream.Position = 0;
            XmlDocument xml = new XmlDocument();
            xml.Load(sourceStream);

            XmlNamespaceManager @namespace = new XmlNamespaceManager(xml.NameTable);
            @namespace.AddNamespace("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            foreach (XmlNode node in xml.SelectNodes("//w:t[contains(., 'FUSION_')]", @namespace))
            {
                node.InnerText = node.InnerText.Replace("FUSION_uid", "0000001");
                node.InnerText = node.InnerText.Replace("FUSION_montant", 19.99m.ToString("C"));
                node.InnerText = node.InnerText.Replace("FUSION_debut", DateTime.Today.ToLongDateString());
            }

            sourceStream.Position = 0;
            xml.Save(sourceStream);
        }
    }
}
