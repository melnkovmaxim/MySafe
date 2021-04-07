using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MySafe.Core;
using MySafe.Data.Xamarin;
using MySafe.Domain.Repositories;
using Xamarin.Forms.Internals;

namespace MySafe.Tests
{
    public class Ioc
    {
        private static readonly string[] SERVICE_NAME_ENDINGS = {"Service", "Repository"};
        private static readonly IServiceProvider _serviceProvider;

        static Ioc() 
        {
            var services = new ServiceCollection();

            typeof(Presentation.Ioc).Assembly.GetTypes()
                .Where(t => SERVICE_NAME_ENDINGS.Any(t.Name.EndsWith) && t.IsClass)
                .ForEach(service =>
                {
                    var interfaces = service.GetInterfaces();
                    var ownInterface = interfaces.FirstOrDefault(x => x.Name == $"I{service.Name}");

                    if (service == typeof(SecureStorageRepository))
                    {
                        services.AddTransient<Mock<ISecureStorageRepository>>();
                    }
                    else
                    {
                        if (interfaces.Any(x => x == typeof(ISingletonService)))
                            AddSingleton(services, service, ownInterface);

                        AddTransient(services, service, ownInterface);
                    }
                });

            services.AddTransient(provider => provider.GetService<Mock<ISecureStorageRepository>>().Object);

            _serviceProvider = services.BuildServiceProvider();
        }

        private static void AddTransient(ServiceCollection services, Type serviceType, Type interfaceType = null)
        {
            if (interfaceType == null)
                services.AddTransient(serviceType);
            else
                services.AddTransient(interfaceType, serviceType);
        }

        private static void AddSingleton(ServiceCollection services, Type serviceType, Type interfaceType = null)
        {
            if (interfaceType == null)
                services.AddSingleton(serviceType);
            else
                services.AddSingleton(interfaceType, serviceType);
        }

        public static T Resolve<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}