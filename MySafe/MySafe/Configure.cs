using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using DryIoc;
using FluentValidation;
using MediatR;
using MySafe.Business.Services;
using MySafe.Business.Services.Abstractions;
using MySafe.Data.Abstractions;
using MySafe.Presentation.Repositories;
using MySafe.Presentation.ViewModels;
using MySafe.Presentation.Views;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using RestSharp;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using ImTools;
using MediatR.Pipeline;
using MySafe.Business.MapperProfiles;
using MySafe.Business.Mediator;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Business.Mediator.Documents.CreateDocumentCommand;
using MySafe.Business.Mediator.Documents.DocumentInfoQuery;
using MySafe.Business.Mediator.Folders.FolderInfoQuery;
using MySafe.Business.Mediator.Pipelines;
using MySafe.Business.Mediator.Users.SignInCommand;
using MySafe.Business.Mediator.Users.TwoFactorAuthenticationCommand;
using MySafe.Core.Entities.Responses;
using MySafe.Core.Entities.Responses.Abstractions;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace MySafe
{
    public static class Configure
    {
        public static IContainerRegistry AddServices(this IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IPasswordManagerService, PasswordManager>();
            containerRegistry.Register<ISecureStorageRepository, SecureStorageRepository>();
            containerRegistry.Register<IDeviceAuthService, DeviceAuthService>();
            containerRegistry.Register<IRestClient, RestClientWrapper>();

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

            //    containerRegistry.RegisterForNavigation(viewModel page.Name);
            //}

            containerRegistry.RegisterForNavigation<DeviceAuthPage, DeviceAuthViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();
            containerRegistry.RegisterForNavigation<DocumentPage, DocumentViewModel>();
            containerRegistry.RegisterForNavigation<NotePage, NoteViewModel>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInViewModel>();
            containerRegistry.RegisterForNavigation<TwoFactorPage, TwoFactorViewModel>();
            containerRegistry.RegisterForNavigation<FolderPage, FolderViewModel>();
            containerRegistry.RegisterForNavigation<TrashFolderPage, TrashFolderViewModel>();

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

                cfg.AddMaps(typeof(ApiProfile));
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

            var mediatrInterfaces = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(IPipelineBehavior<,>),
                typeof(BearerRequestBase<>),
                typeof(IRequestPostProcessor<,>),
                typeof(IRequestExceptionHandler<,>),
                typeof(IValidator<>)
            };

            var referencedAssemblies = Assembly.GetAssembly(typeof(Configure)).GetReferencedAssemblies();
            var types = referencedAssemblies
                .Where(x => x.Name.Contains(nameof(MySafe.Business)))
                .Select(Assembly.Load)
                .SelectMany(x => x.GetTypes())
                .ToList();

            foreach (var @interface in mediatrInterfaces)
            {
                container.RegisterMany(types
                        .Where(t => !t.IsAbstract && t.GetInterfaces()
                            .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == @interface)),
                    serviceTypeCondition: Registrator.Interfaces);

            }

            container.Register(typeof(BearerPreRequestHandler<>));

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
        public static DeviceAuthViewModel DeviceAuthViewModel => Ioc.Resolve<DeviceAuthViewModel>();
        public static MainViewModel MainViewModel => Ioc.Resolve<MainViewModel>();
        public static DocumentViewModel DocumentViewModel => Ioc.Resolve<DocumentViewModel>();
        public static SignInViewModel SignInViewModel => Ioc.Resolve<SignInViewModel>();
        public static TwoFactorViewModel TwoFactorViewModel => Ioc.Resolve<TwoFactorViewModel>();
        public static FolderViewModel FolderViewModel => Ioc.Resolve<FolderViewModel>();
        public static TrashFolderViewModel TrashViewModel => Ioc.Resolve<TrashFolderViewModel>();
    }
}
