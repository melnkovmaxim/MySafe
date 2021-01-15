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
using MySafe.Views;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Xamarin.Forms;

namespace MySafe
{
    public static class Ioc
    {

        public static IContainerRegistry RegisterServices(this IContainerRegistry container)
        {
            container.Register<IPasswordManagerService, PasswordManagerService>();
            container.Register<ILoginService, LoginService>();
            container.Register<IRegisterService, RegisterService>();

            return container;
        }

        public static IContainerRegistry RegisterNavigation(this IContainerRegistry container)
        {
            container.RegisterForNavigation<NavigationPage>();
            container.RegisterForNavigation<AuthPage, AuthViewModel>();
            container.RegisterForNavigation<MainPage, MainViewModel>();

            return container;
        }

        public static T Resolve<T>() => PrismApplicationBase.Current.Container.Resolve<T>();
    }

    public class VmLocator
    {
        public static AuthViewModel AuthViewModel => Ioc.Resolve<AuthViewModel>();
    }
}
