using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Profi.Infra
{
    public class Bus
    {
        private static List<Type> handlers = new();
        private readonly IServiceProvider serviceProvider;

        public Bus(IServiceProvider serviceProvider)
        {
            ChargerHandlers();
            this.serviceProvider = serviceProvider;
        }

        public async Task<object?> DispatchMessage<TMessage>(TMessage message) where TMessage : class, IMessage
        {
            var handlerType = handlers.Find(h => h.GetInterfaces().Any(i => i.GetGenericArguments().Contains(message.GetType())));
            if (handlerType is not null)
            {
                var handlerInstance = GetHandlerFromType<TMessage>(handlerType);
                if (handlerInstance is not null)
                {
                    return await handlerInstance.HandleMessage(message);
                }
            }
            return null;
        }

        private IHandler<TMessage>? GetHandlerFromType<TMessage>(Type t) where TMessage : class, IMessage
        {
            var ctor = t.GetConstructors(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault();
            if (ctor is not null)
            {
                List<object> ctorParams = new();
                using var scope = serviceProvider.CreateScope();
                foreach (var p in ctor.GetParameters())
                {
                    var ctorParamInstance = scope.ServiceProvider.GetService(p.ParameterType);
                    if (ctorParamInstance is not null)
                    {
                        ctorParams.Add(ctorParamInstance);
                    }
                }
                return ctor.Invoke(ctorParams.ToArray()) as IHandler<TMessage>;
            }

            return null;
        }

        private static void ChargerHandlers()
        {
            string? repExecution = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
            if (!string.IsNullOrWhiteSpace(repExecution))
            {
                foreach (string fichier in Directory.GetFiles(repExecution, "*Profi*.dll"))
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
                                    handlers.Add(t);
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