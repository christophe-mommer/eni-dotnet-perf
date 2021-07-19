using System;
using System.Collections.Generic;

namespace Nouveau_dossier
{
    public partial class Contrat
    {
        public string Uid { get; set; }
        public string Titulaire { get; set; }
        public decimal Montant { get; set; }
        public DateTime? Debut { get; set; }
        public string Reduction { get; set; }

        public virtual Personne TitulaireNavigation { get; set; }
    }
}
