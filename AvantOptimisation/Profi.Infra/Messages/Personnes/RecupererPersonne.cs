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
