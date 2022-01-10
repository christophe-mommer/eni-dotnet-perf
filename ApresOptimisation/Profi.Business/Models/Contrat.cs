namespace Profi.Business.Models
{
    public class Contrat
    {
        public string Uid { get; set; } = "";
        public string UidTitulaire { get; set; } = "";
        public decimal Montant { get; set; }
        public DateTime Debut { get; set; }
        public string? Reduction { get; set; }
        public string? DocumentBase64 { get; set; }

        public Personne TitulaireNavigation { get; set; } = default!;
    }
}
