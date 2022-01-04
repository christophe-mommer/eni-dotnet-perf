namespace Profi.Infra
{
    public interface IHandler<in TMessage> where TMessage : class, IMessage
    {
        Task<object?> HandleMessage(TMessage message);
    }
}
