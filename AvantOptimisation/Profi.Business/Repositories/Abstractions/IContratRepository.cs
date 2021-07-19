using Profi.Business.Data.Models;
using System.Collections.Generic;

namespace Profi.Business.Repositories.Abstractions
{
    public interface IContratRepository
    {
        Contrat? Recuperer(string UIDContrat);
        List<Contrat> RecupererListe(string RestrictionUIDPersonne);
        List<string> RequeteComplexe();
    }
}