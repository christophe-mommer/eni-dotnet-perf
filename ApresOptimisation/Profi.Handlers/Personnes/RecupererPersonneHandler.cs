using Profi.Data.Abstractions;
using Profi.Infra;
using Profi.Infra.Messages.Personnes;

namespace Profi.Handlers.Personnes
{
    public class RecupererPersonneHandler : IHandler<RecupererPersonne>
    {
        private readonly IPersonneRepository repo;

        public RecupererPersonneHandler(
            IPersonneRepository repo)
        {
            this.repo = repo;
        }

        public Task<object?> HandleMessage(RecupererPersonne message)
        {
            object? result = repo.Recuperer(message.Id);
            return Task.FromResult(result);
        }
    }
}
