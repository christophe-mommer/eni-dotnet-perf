using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profi.Business.Data.Models
{
    public class Personne
    {
        public Personne()
        {
            Contrats = new HashSet<Contrat>();
        }

        public string Uid { get; set; } = "";
        public string Nom { get; set; } = "";
        public string Prenom { get; set; } = "";
        public string Description { get; set; } = "";

        public virtual ICollection<Contrat> Contrats { get; set; }
    }
}
