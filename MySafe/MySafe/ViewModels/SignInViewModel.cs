using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Mediator.SignIn;
using MySafe.ViewModels.Abstractions;
using NetStandardCommands;
using Prism.Navigation;

namespace MySafe.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        public string Login { get; set; }
        public string Password { get; set; }


        public SignInViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        private AsyncCommand _signInCommand;
        public AsyncCommand SignInCommand => _signInCommand ??= new AsyncCommand(async () =>
        {
            await Task.Run(() => Ioc.Resolve<IMediator>().Send(new SignInCommand(Login, Password)));
        }, () => true, !SignInCommand.IsExecuting);
    }
}
