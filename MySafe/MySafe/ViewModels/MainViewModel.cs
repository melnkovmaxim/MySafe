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
        public int Progress => Convert.ToInt32(Math.Floor(_usedCapacity / ((double) _maxCapacity / 100)));
        public string SafeSizeInfo => $"{_usedCapacity}/{_maxCapacity} MB";

        private int _maxCapacity;
        private int _usedCapacity;

        private readonly IMediator _mediator;
        private AsyncCommand _signOutCommand;
        private DelegateCommand _loadedCommand;

        public MainViewModel(INavigationService navigationService, IMediator mediator) 
            : base(navigationService)
        {
            _mediator = mediator;
        }

        public DelegateCommand LoadedCommand => _loadedCommand ??= new DelegateCommand(async () =>
        {
            var queryResponse = await _mediator.Send(new SafeInfoQuery(_jwtToken));

            if (queryResponse.HasError)
            {
                Error = queryResponse.Error;
                return;
            }

            _maxCapacity = Convert.ToInt32(Math.Floor(queryResponse.Capacity));
            _usedCapacity = Convert.ToInt32(Math.Floor(queryResponse.UsedCapacity));
        });

        public AsyncCommand SignOutCommand => _signOutCommand ??= new AsyncCommand(async () =>
        {
            _ = _mediator.Send(new SignOutCommand(_jwtToken));
            await _navigationService.NavigateAsync(nameof(AuthPage));
        });
    }
}
