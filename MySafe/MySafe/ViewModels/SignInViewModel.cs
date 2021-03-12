using MediatR;
using MySafe.Business.Mediator.Users.SignInCommand;
using MySafe.Business.Services.Abstractions;
using MySafe.Core.Commands;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Presentation.Views;
using Prism.Navigation;

namespace MySafe.Presentation.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private AsyncCommand _signInCommand;

        public string Login { get; set; }
        public string Password { get; set; }


        public SignInViewModel(INavigationService navigationService, IMediator mediator)
            :base(navigationService)
        {
            _mediator = mediator;
        }

        public AsyncCommand SignInCommand => _signInCommand ??= new AsyncCommand(async () =>
        {
            //Ioc.Resolve<IPrintService>().ShowPrinterWebView();

            //return;
            var response = await _mediator.Send(new SignInCommand(Login, Password));

            if (response.HasError)
            {
                Error = response.Error;
                return;
            }

            await _navigationService.NavigateAsync(nameof(TwoFactorPage));
        });
    }
}
