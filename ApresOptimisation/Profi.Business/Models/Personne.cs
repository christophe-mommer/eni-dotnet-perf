namespace Profi.Business.Models
{
    public class Personne
    {
        public string Uid { get; set; } = "";
        public string Nom { get; set; } = "";
        public string Prenom { get; set; } = "";
        public string Description { get; set; } = "";
        public List<Contrat> Contrats { get; set; } = new();
    }
}
