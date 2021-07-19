using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profi.Business.Data.Models
{
    public class Contrat
    {
        public string Uid { get; set; } = "";
        public string Titulaire { get; set; } = "";
        public decimal Montant { get; set; }
        public DateTime? Debut { get; set; }
        public string? Reduction { get; set; }
        public string? DocumentBase64 { get; set; }

        public virtual Personne TitulaireNavigation { get; set; } = default!;
    }
}
