namespace Profi.Infra.Messages.Contrats;

public class FusionnerContrat : IMessage
{
    public FusionnerContrat(string id)
    {
        Id = id;
    }

    public string Id { get; }
}
