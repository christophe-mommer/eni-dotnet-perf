using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profi.Infra
{
    public class Message
    {
        public string Ordre { get; }

        public readonly Dictionary<string, string> Parametres 
            = new Dictionary<string, string>();

        public Message(string ordre) 
            => Ordre = ordre;

        public void AjouterParametre(string code, string valeur) 
            => Parametres.Add(code, valeur);
    }
}
