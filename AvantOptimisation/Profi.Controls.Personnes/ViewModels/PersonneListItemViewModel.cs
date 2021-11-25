using Profi.Business.Models;

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
