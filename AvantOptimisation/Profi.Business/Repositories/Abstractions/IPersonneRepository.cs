using Profi.Business.Data.Models;
using System.Collections.Generic;

namespace Profi.Business.Repositories.Abstractions
{
    public interface IPersonneRepository
    {
        Personne? Recuperer(string UIDPersonne);
        List<Personne> RecupererListe();
    }
}