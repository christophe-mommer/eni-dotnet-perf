using Profi.Data.Abstractions;
using Profi.Infra;
using Profi.Infra.Messages.Contrats;

namespace Profi.Handlers.Contrats
{
    public class RecupererListeParPersonneHandler : IHandler<RecupererListeParPersonne>
    {
        private readonly IContratRepository repo;

        public RecupererListeParPersonneHandler(
            IContratRepository repo)
        {
            this.repo = repo;
        }

        public Task<object?> HandleMessage(RecupererListeParPersonne message)
        {
            object result = repo.RecupererListe(message.PersonneId);
            return Task.FromResult(result);
        }
    }
}
