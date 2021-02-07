using System;
using MediatR;
using MySafe.Mediator.SafeInfo;
using MySafe.Mediator.SignOut;
using MySafe.Repositories.Abstractions;
using MySafe.Services.Abstractions;
using MySafe.ViewModels.Abstractions;
using MySafe.Views;
using NetStandardCommands;
using Prism.Commands;
using Prism.Navigation;

namespace MySafe.ViewModels
{
    public class MainViewModel : AuthorizedViewModelBase
    {
        public double Progress { get; set; }
        public string SafeSizeInfo { get; set; }

        private int _maxCapacity;
        private int _usedCapacity;

        private readonly IMediator _mediator;
        private AsyncCommand _signOutCommand;

        public MainViewModel(INavigationService navigationService, IMediator mediator) 
            : base(navigationService)
        {
            _mediator = mediator;

            _loadedCommand ??= new DelegateCommand(ActionAfterLoadPage);
        }

        private async void ActionAfterLoadPage()
        {
            var queryResponse = await _mediator.Send(new SafeInfoQuery(_jwtToken));

            if (queryResponse.HasError)
            {
                Error = queryResponse.Error;
                return;
            }

            _maxCapacity = Convert.ToInt32(Math.Floor(queryResponse.Capacity));
            _usedCapacity = Convert.ToInt32(Math.Floor(queryResponse.UsedCapacity));

            Progress = Math.Floor( (double) _maxCapacity / _usedCapacity ) / 100;
            SafeSizeInfo = $"{_usedCapacity}/{_maxCapacity} MB";
        }

        public AsyncCommand SignOutCommand => _signOutCommand ??= new AsyncCommand(async () =>
        {
            _ = _mediator.Send(new SignOutCommand(_jwtToken));
            await _navigationService.NavigateAsync(nameof(AuthPage));
        });
    }
}
