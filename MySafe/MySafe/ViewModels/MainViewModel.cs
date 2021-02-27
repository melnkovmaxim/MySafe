using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MediatR;
using MySafe.Mediator.Safe.SafeInfo;
using MySafe.Mediator.Users.SignOut;
using MySafe.Models;
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

        public string Doc1 { get; set; }
        public string Doc2 { get; set; }
        public string Doc3 { get; set; }
        public string Doc4 { get; set; }
        public string Doc5 { get; set; }
        public string Doc6 { get; set; }

        public int Doc1Id { get; set; }
        public int Doc2Id { get; set; }
        public int Doc3Id { get; set; }
        public int Doc4Id { get; set; }
        public int Doc5Id { get; set; }
        public int Doc6Id { get; set; }

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

            Progress = Math.Floor((double) _maxCapacity / _usedCapacity) / 100;
            SafeSizeInfo = $"{_usedCapacity}/{_maxCapacity} MB";
            for (var i = 0; i < queryResponse.Folders.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        Doc1 = queryResponse.Folders[i].Name;
                        Doc1Id = queryResponse.Folders[i].Id;
                        break;
                    case 1:
                        Doc2 = queryResponse.Folders[i].Name;
                        break;
                    case 2:
                        Doc3 = queryResponse.Folders[i].Name;
                        break;
                    case 3:
                        Doc4 = queryResponse.Folders[i].Name;
                        break;
                    case 4:
                        Doc5 = queryResponse.Folders[i].Name;
                        break;
                    case 5:
                        Doc6 = queryResponse.Folders[i].Name;
                        break;
                }
            }
        }

        public DelegateCommand NavigCommand => new DelegateCommand( async () =>
            {
                await _navigationService.NavigateAsync(nameof(DocumentPage), new NavigationParameters {{"id", Doc1Id}});
            });


        public AsyncCommand SignOutCommand => _signOutCommand ??= new AsyncCommand(async () =>
        {
            _ = _mediator.Send(new SignOutCommand(_jwtToken));
            await _navigationService.NavigateAsync(nameof(AuthPage));
        });
    }
}
