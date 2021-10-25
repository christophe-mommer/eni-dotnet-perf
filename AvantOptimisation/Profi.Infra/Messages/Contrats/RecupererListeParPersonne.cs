using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profi.Infra.Messages.Contrats
{
    public class RecupererListeParPersonne : IMessage
    {
        public RecupererListeParPersonne(string personneId)
        {
            PersonneId = personneId;
        }

        public string PersonneId { get; }
    }
}
