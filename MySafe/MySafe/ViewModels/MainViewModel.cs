using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MediatR;
using MySafe.Mediator.Safe.SafeInfo;
using MySafe.Mediator.Users.SignOut;
using MySafe.Models;
using MySafe.Models.Responses;
using MySafe.Repositories.Abstractions;
using MySafe.Services.Abstractions;
using MySafe.ViewModels.Abstractions;
using MySafe.Views;
using NetStandardCommands;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms.Internals;

namespace MySafe.ViewModels
{
    public class MainViewModel : AuthorizedViewModelBase
    {
        public double Progress { get; set; }
        public string SafeSizeInfo { get; set; }


        private int _maxCapacity;
        private int _usedCapacity;

        public ObservableCollection<FolderResponse> Folders { get; set; }

        private readonly IMediator _mediator;
        private AsyncCommand _signOutCommand;
        private AsyncCommand<FolderResponse> _moveToFolderCommand;

        public MainViewModel(INavigationService navigationService, IMediator mediator)
            : base(navigationService)
        {
            _mediator = mediator;
            Folders = new ObservableCollection<FolderResponse>();
        }

        protected override async void ActionAfterLoadPage()
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

            Folders.Clear();
            //queryResponse.Folders.ForEach(Folders.Add);
            queryResponse.Folders.ForEach(x => {
                x.Name = x.Name.Split(":").FirstOrDefault();
                Folders.Add(x);
            });
        }

        public AsyncCommand<FolderResponse> MoveToFolderCommand => 
            _moveToFolderCommand ??= new AsyncCommand<FolderResponse>(async (folder) =>
        {
            var @params = GetItemNaviigationParams(folder.Id, folder.Name);
            await _navigationService.NavigateAsync(nameof(FolderPage), @params);
        });

        public AsyncCommand SignOutCommand => _signOutCommand ??= new AsyncCommand(async () =>
        {
            _ = _mediator.Send(new SignOutCommand(_jwtToken));
            await _navigationService.NavigateAsync(nameof(AuthPage));
        });

    }
}
