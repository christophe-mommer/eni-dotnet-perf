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
