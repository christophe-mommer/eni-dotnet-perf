using System;
using System.Collections.Generic;

namespace Nouveau_dossier
{
    public partial class Personne
    {
        public Personne()
        {
            Contrats = new HashSet<Contrat>();
        }

        public string Uid { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Contrat> Contrats { get; set; }
    }
}
