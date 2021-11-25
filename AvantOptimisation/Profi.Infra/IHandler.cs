namespace Profi.Infra
{
    public interface IHandler<TMessage> where TMessage : class, IMessage
    {
        Task<object> HandleMessage(TMessage message);
    }
}
