using System.Reflection;

namespace Profi.Infra
{
    public class Bus
    {
        private static Bus? _bus;
        public static Bus Current
        {
            get
            {
                if (_bus == null)
                {
                    _bus = new Bus();
                }
                return _bus;
            }
        }

        private static List<object> handlers = new();

        private Bus()
        {
            ChargerHandlers();
        }

        public async Task<object?> DispatchMessage<TMessage>(TMessage message) where TMessage : class, IMessage
        {
            var handler = handlers.FirstOrDefault(h => h.GetType().GetInterfaces().Any(i => i.GetGenericArguments().Contains(message.GetType())));
            if (handler is not null)
            {
                return await (handler as IHandler<TMessage>)?.HandleMessage(message) ?? Task.FromResult<object?>(null);
            }
            return null;
        }

        private static void ChargerHandlers()
        {
            string? repExecution = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
            if (!string.IsNullOrWhiteSpace(repExecution))
            {
                foreach (string fichier in Directory.GetFiles(repExecution))
                {
                    try
                    {
                        Assembly assembly = Assembly.LoadFrom(fichier);

                        foreach (Type t in assembly.GetTypes())
                        {
                            try
                            {
                                if (t.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IHandler<>)))
                                {
                                    var handler = Activator.CreateInstance(t);
                                    if (handler is not null)
                                    {
                                        handlers.Add(handler);
                                    }
                                }
                            }
                            catch
                            {
                                // Catch vide si on arrive pas à trater un type particulier
                            }
                        }
                    }
                    catch
                    {
                        // Catch vide, rien à faire si l'assembly ne peut être chargée
                    }
                }
            }
        }
    }
}