using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using AutoMapper;
using DryIoc;
using FluentValidation;
using MediatR;
using MySafe.Repositories;
using MySafe.Repositories.Abstractions;
using MySafe.Services;
using MySafe.Services.Abstractions;
using MySafe.ViewModels;
using MySafe.ViewModels.Abstractions;
using MySafe.Views;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using RestSharp;
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
            containerRegistry.Register<ISecureStorageRepository, SecureStorageRepository>();
            containerRegistry.Register<IDeviceAuthService, DeviceAuthService>();
            containerRegistry.RegisterInstance(typeof(IRestClient), new RestClient("https://mysafeonline.com/"));//"http://username228-001-site1.itempurl.com/"));//""));

            return containerRegistry;
        }

        public static IContainerRegistry AddRepositories(this IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ISecureStorageRepository, SecureStorageRepository>();

            return containerRegistry;
        }

        public static IContainerRegistry AddNavigation(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            //var types = typeof(Configure).Assembly.GetTypes();
            //var pages = types
            //    .Where(t => t.BaseType == typeof(ContentPage) && t.Name.EndsWith("Page"));  
            //var viewModels = types
            //    .Where(t => t.BaseType == typeof(ViewModelBase) || t.Name.EndsWith("ViewModel"))
            //    .ToList();

            //foreach (var page in pages)
            //{
            //    var pageName = page.Name.Replace("Page", string.Empty);
            //    var viewModel = viewModels.FirstOrDefault(vm => vm.Name.StartsWith(pageName));

            //    containerRegistry.RegisterForNavigation<typeof(NavigationPage)>();
            //}

            containerRegistry.RegisterForNavigation<AuthPage, AuthViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();
            containerRegistry.RegisterForNavigation<DocumentPage, DocumentViewModel>();
            containerRegistry.RegisterForNavigation<TaxPage, TaxViewModel>();
            containerRegistry.RegisterForNavigation<HealthPage, HealthViewModel>();
            containerRegistry.RegisterForNavigation<BinPage, BinViewModel>();
            containerRegistry.RegisterForNavigation<UtilPage, UtilViewModel>();
            containerRegistry.RegisterForNavigation<NotePage, NoteViewModel>();
            containerRegistry.RegisterForNavigation<EstatePage, EstateViewModel>();
            containerRegistry.RegisterForNavigation<OtherPage, OtherViewModel>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInViewModel>();
            containerRegistry.RegisterForNavigation<TwoFactorPage, TwoFactorViewModel>();

            return containerRegistry;
        }

        public static IContainerRegistry AddMapper(this IContainerRegistry containerRegistry)
        { 
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                var profiles = typeof(Configure).Assembly
                    .GetTypes()
                    .Where(t => t.GetBaseType() == typeof(Profile));
                
                foreach (var profile in profiles)
                {
                    cfg.AddMaps(profile);
                }
            });

            containerRegistry.RegisterInstance(typeof(IMapper), new Mapper(mapperConfig));

            return containerRegistry;
        }

        public static IContainerRegistry AddMediatr(this IContainerRegistry containerRegistry)
        {
            var container = containerRegistry.GetContainer();

            container.RegisterDelegate<ServiceFactory>(r => r.Resolve);
            container.RegisterMany(
                new[] { typeof(IMediator).GetAssembly(), typeof(Configure).GetAssembly() }, Registrator.Interfaces);

            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(IPipelineBehavior<,>),
                typeof(AbstractValidator<>)
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                container.RegisterMany(
                    typeof(Configure).Assembly.GetTypes()
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
        public static MainViewModel MainViewModel => Ioc.Resolve<MainViewModel>();
        public static DocumentViewModel DocumentViewModel => Ioc.Resolve<DocumentViewModel>();
        public static SignInViewModel SignInViewModel => Ioc.Resolve<SignInViewModel>();
        public static TwoFactorViewModel TwoFactorViewModel => Ioc.Resolve<TwoFactorViewModel>();
    }
}
