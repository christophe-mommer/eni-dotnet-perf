using Profi.Business.Models;

namespace Profi.Controls.Contrats.ViewModels
{
    internal class ContratListItemViewModel
    {
        internal Contrat Payload { get; }
        private bool marquer;

        public ContratListItemViewModel(Contrat payload, bool marquer)
        {
            this.Payload = payload;
            this.marquer = marquer;
        }

        public override string ToString()
        {
            string reduction = string.IsNullOrEmpty(Payload.Reduction) ? string.Empty : " avec réduction " + Payload.Reduction;
            string prefix = marquer ? "* " : string.Empty;
            return string.Concat(prefix, "Le ", Payload.Debut.ToShortDateString(), " pour ", Payload.Montant.ToString("C"), reduction, " (", Payload.Uid, ")");
        }
    }
}
