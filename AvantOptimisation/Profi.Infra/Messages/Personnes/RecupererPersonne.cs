using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profi.Infra.Messages.Personnes
{
    public class RecupererPersonne : IMessage
    {
        public string Id { get; }

        public RecupererPersonne(string id)
        {
            Id = id;
        }
    }
}
