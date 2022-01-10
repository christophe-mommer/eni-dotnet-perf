using Profi.Business.Models;

namespace Profi.Data.Abstractions
{
    public interface IPersonneRepository
    {
        List<Personne> RecupererListe();
        Personne? Recuperer(string UIDPersonne);
    }
}
