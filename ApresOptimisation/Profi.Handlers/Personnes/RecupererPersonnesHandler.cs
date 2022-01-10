using Profi.Data.Abstractions;
using Profi.Infra;
using Profi.Infra.Messages.Personnes;

namespace Profi.Handlers.Personnes
{
    public class RecupererPersonnesHandler : IHandler<RecupererPersonnes>
    {
        private readonly IPersonneRepository repo;

        public RecupererPersonnesHandler(IPersonneRepository repo)
        {
            this.repo = repo;
        }
        public Task<object?> HandleMessage(RecupererPersonnes message)
        {
            object result = repo.RecupererListe();
            return Task.FromResult(result);
        }
    }
}
