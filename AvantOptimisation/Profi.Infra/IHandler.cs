using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profi.Infra
{
    public interface IHandler<TMessage> where TMessage : class, IMessage
    {
        Task<object> HandleMessage(TMessage message);
    }
}
