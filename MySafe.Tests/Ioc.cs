using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MySafe.Services.Abstractions;
using Xamarin.Forms.Internals;

namespace MySafe.Tests
{
    public class Ioc
    {
        private static readonly string[] SERVICE_NAME_ENDINGS = {"Service", "Repository"};

        static Ioc()
        {
            var services = new ServiceCollection();

            typeof(Ioc).Assembly.GetTypes()
                .Where(t => SERVICE_NAME_ENDINGS.Any(t.Name.EndsWith))
                .ForEach(service =>
                {
                    var interfaces = service.GetInterfaces();
                    var ownInterface = interfaces.FirstOrDefault(x => x.Name == $"I{service.Name}");

                    if (interfaces.Any(x => x == typeof(ISingletonService)))
                    {
                        AddSingleton(services, service, ownInterface);
                    }

                    AddTransient(services, service, ownInterface);
                });
                
        }

        private static void AddTransient(ServiceCollection services, Type serviceType, Type interfaceType = null)
        {
            if (interfaceType == null)
            {
                services.AddTransient(serviceType);
            }
            else
            {
                services.AddTransient(interfaceType, serviceType);
            }
        }

        private static void AddSingleton(ServiceCollection services, Type serviceType, Type interfaceType = null)
        {
            if (interfaceType == null)
            {
                services.AddSingleton(serviceType);
            }
            else
            {
                services.AddSingleton(interfaceType, serviceType);
            }
        }
    }
}
