using Profi.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profi.Controls.Personnes.ViewModels
{
    internal class PersonneListItemViewModel
    {
        internal Personne Payload { get; }

        public PersonneListItemViewModel(Personne payload)
        {
            this.Payload = payload;
        }

        public override string ToString()
        {
            return $"{Payload.Nom} {Payload.Prenom}";
        }
    }
}
