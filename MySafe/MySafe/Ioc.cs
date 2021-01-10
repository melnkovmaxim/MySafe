using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DryIoc;
using MediatR;
using MySafe.Services;
using MySafe.Services.Abstractions;
using MySafe.ViewModels;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;

namespace MySafe
{
    public class Ioc
    {
        private static readonly IContainer _container;
        private static readonly IContainerProvider _containerProvider;

        static Ioc()
        {
            _containerProvider = PrismApplicationBase.Current.Container;
            _container = _containerProvider.GetContainer();
            
            //_container.RegisterDelegate<ServiceFactory>(r => r.Resolve);
            //_container.UseInstance<TextWriter>(writer);
            //_container.RegisterMany(new[] { typeof(IMediator).GetAssembly(), typeof(Ping).GetAssembly() }, Registrator.Interfaces);
            // _container.RegisterMany<TestService>(Reuse.Singleton);
        }

        public static T Resolve<T>() => _containerProvider.Resolve<T>();
    }

    public class VmLocator
    {
        public static AuthViewModel AuthViewModel => Ioc.Resolve<AuthViewModel>();
    }
}
