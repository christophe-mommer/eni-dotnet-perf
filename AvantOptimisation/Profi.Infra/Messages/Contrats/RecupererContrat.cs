using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profi.Infra.Messages.Contrats
{
    public class RecupererContrat : IMessage
    {
        public RecupererContrat(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
