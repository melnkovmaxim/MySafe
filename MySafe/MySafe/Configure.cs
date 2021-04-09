using System.Linq;
using System.Reflection;
using AutoMapper;
using DryIoc;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using MySafe.Data.Xamarin;
using MySafe.Domain.Repositories;
using MySafe.Domain.Services;
using MySafe.Presentation.MapperProfiles;
using MySafe.Presentation.ViewModels;
using MySafe.Presentation.Views;
using MySafe.Services.MapperProfiles;
using MySafe.Services.Mediator;
using MySafe.Services.Mediator.Abstractions;
using MySafe.Services.Services;
using MySafe.Services.Xamarin;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using RestSharp;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace MySafe.Presentation
{
    public static class Configure
    {
        public static IContainerRegistry AddServices(this IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IPasswordManagerService, PasswordManager>();
            containerRegistry.Register<ISecureStorageRepository, SecureStorageRepository>();
            containerRegistry.Register<IDeviceAuthService, DeviceAuthService>();
            containerRegistry.Register<IRestClient, RestClientWrapper>();
            containerRegistry.Register<IFileService, FileService>();
            containerRegistry.Register<IPermissionService, PermissionService>();
            containerRegistry.Register<IJwtService, JwtService>();

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
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterViewModel>();
            containerRegistry.RegisterForNavigation<NoteEditPage, NoteEditViewModel>();

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

                //TODO: убрать регистрацию константно и сделать динамически через ассембли, как для медиатра
                cfg.AddMaps(typeof(MediatorQueryCommandProfile));
                cfg.AddMaps(typeof(PresentationProfile));
                cfg.AddMaps(typeof(CommandQueryProfile));
            });

            containerRegistry.RegisterInstance(typeof(IMapper), new Mapper(mapperConfig));

            return containerRegistry;
        }

        public static IContainerRegistry AddMediatr(this IContainerRegistry containerRegistry)
        {
            
            var container = containerRegistry.GetContainer();
            container.RegisterDelegate<ServiceFactory>(r => r.Resolve);

            //TODO: пофиксить для ios-версии
            container.RegisterMany(
                new[] {typeof(IMediator).GetAssembly(), typeof(Configure).GetAssembly()}, Registrator.Interfaces);

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
                .Where(x => x.Name.Contains(nameof(Services)))
                .Select(Assembly.Load)
                .SelectMany(x => x.GetTypes())
                .ToList();

            foreach (var @interface in mediatrInterfaces)
                container.RegisterMany(types
                        .Where(t => !t.IsAbstract && t.GetInterfaces()
                            .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == @interface)),
                    serviceTypeCondition: Registrator.Interfaces);

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
        public static T Resolve<T>()
        {
            return PrismApplicationBase.Current.Container.Resolve<T>();
        }
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
        public static NoteViewModel NoteViewModel => Ioc.Resolve<NoteViewModel>();
        public static NoteEditViewModel NoteEditViewModel => Ioc.Resolve<NoteEditViewModel>();
        public static RegisterViewModel RegisterViewModel => Ioc.Resolve<RegisterViewModel>();
    }
}