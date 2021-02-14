using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MySafe.Repositories;
using MySafe.Repositories.Abstractions;
using MySafe.Services;
using MySafe.Services.Abstractions;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms.Internals;

namespace MySafe.Tests
{
    public class Ioc
    {
        private static readonly string[] SERVICE_NAME_ENDINGS = { "Service", "Repository" };
        private static readonly IServiceProvider _serviceProvider;

        static Ioc()
        {
            var services = new ServiceCollection();
            
            services.AddTransient<ISecureStorage, SecureStorageWrapper>();
            services.AddSingleton<Mock<IAsyncDelayerService>>();
            services.AddSingleton<IAsyncDelayerService>(provider => provider.GetService<Mock<IAsyncDelayerService>>().Object);


            typeof(MySafe.Configure).Assembly.GetTypes()
                .Where(t => SERVICE_NAME_ENDINGS.Any(t.Name.EndsWith) && t.IsClass)
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
            
                        
            _serviceProvider = services.BuildServiceProvider();

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

        public static T Resolve<T>() => _serviceProvider.GetRequiredService<T>();
    }
}
