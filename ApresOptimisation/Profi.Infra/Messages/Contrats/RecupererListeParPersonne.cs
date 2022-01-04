namespace Profi.Infra.Messages.Contrats
{
    public class RecupererListeParPersonne : IMessage
    {
        public RecupererListeParPersonne(string personneId)
        {
            PersonneId = personneId;
        }

        public string PersonneId { get; }
    }
}
