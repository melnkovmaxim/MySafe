using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using DryIoc;
using MediatR;
using MySafe.Services;
using MySafe.Services.Abstractions;
using MySafe.ViewModels;
using MySafe.Views;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace MySafe
{
    public static class Configure
    {
        public static IContainerRegistry AddServices(this IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IPasswordManagerService, PasswordManagerService>();
            containerRegistry.Register<ILoginService, LoginService>();
            containerRegistry.Register<IRegisterService, RegisterService>();

            return containerRegistry;
        }

        public static IContainerRegistry AddNavigation(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<AuthPage, AuthViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();

            return containerRegistry;
        }

        public static IContainerRegistry AddMediatr(this IContainerRegistry containerRegistry)
        {
            var container = containerRegistry.GetContainer();

            container.RegisterDelegate<ServiceFactory>(r => r.Resolve);
            container.RegisterMany(
                new[] { typeof(IMediator).GetAssembly(), typeof(Ioc).GetAssembly() }, Registrator.Interfaces);

            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>)
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                container.RegisterMany(
                    typeof(Ioc).Assembly.GetTypes()
                        .Where(t => t.GetInterfaces()
                            .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == mediatrOpenType)), 
                    serviceTypeCondition: Registrator.Interfaces);
            }

            return containerRegistry;
        }

        public static IContainerRegistry AddApplication(this IContainerRegistry containerRegistry)
        { 
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            return containerRegistry;
        }
    }

    public class Ioc
    {
        public static T Resolve<T>() => PrismApplicationBase.Current.Container.Resolve<T>();
    }

    public class VmLocator
    {
        public static AuthViewModel AuthViewModel => Ioc.Resolve<AuthViewModel>();
    }
}
