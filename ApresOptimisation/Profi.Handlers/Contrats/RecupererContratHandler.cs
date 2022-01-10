using Profi.Data.Abstractions;
using Profi.Data.Implementations;
using Profi.Infra;
using Profi.Infra.Messages.Contrats;

namespace Profi.Business.Handlers.Contrats
{
    public class RecupererContratHandler : IHandler<RecupererContrat>
    {
        private readonly IContratRepository repo;

        public RecupererContratHandler(
            IContratRepository repo)
        {
            this.repo = repo;
        }

        public Task<object?> HandleMessage(RecupererContrat message)
        {
            object? result = new SqlContratRepository().Recuperer(message.Id);
            return Task.FromResult(result);
        }
    }
}
