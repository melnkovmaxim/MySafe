using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DryIoc;
using MySafe.Services;
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

            // _container.RegisterMany<TestService>(Reuse.Singleton);
        }

        public static T Resolve<T>() => _containerProvider.Resolve<T>();
    }

    public class VmLocator
    {
        public static LoginViewModel AuthViewModel => Ioc.Resolve<LoginViewModel>();
    }
}
