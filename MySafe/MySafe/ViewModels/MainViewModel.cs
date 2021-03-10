using MediatR;
using MySafe.Core.Commands;
using MySafe.Core.Entities.Responses;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Presentation.Views;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MySafe.Business.Mediator.Safe.SafeInfoQuery;
using Xamarin.Forms.Internals;

namespace MySafe.Presentation.ViewModels
{
    public class MainViewModel : AuthorizedViewModelBase
    {
        public double Progress { get; set; }
        public string SafeSizeInfo { get; set; }


        private int _maxCapacity;
        private int _usedCapacity;

        public ObservableCollection<Folder> Folders { get; set; }

        private readonly IMediator _mediator;
        private AsyncCommand _signOutCommand;
        private AsyncCommand<Folder> _moveToFolderCommand;

        public MainViewModel(INavigationService navigationService, IMediator mediator)
            : base(navigationService)
        {
            _mediator = mediator;
            Folders = new ObservableCollection<Folder>();
        }

        protected override async Task ActionAfterLoadPage()
        {
            var queryResponse = await _mediator.Send(new SafeInfoQuery());

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

        public AsyncCommand<Folder> MoveToFolderCommand => 
            _moveToFolderCommand ??= new AsyncCommand<Folder>(async (folder) =>
        {
            var @params = GetItemNaviigationParams(folder.Id, folder.Name);
            await _navigationService.NavigateAsync(nameof(FolderPage), @params);
        });

        public AsyncCommand SignOutCommand => _signOutCommand ??= new AsyncCommand(async () =>
        {
            //_ = _mediator.Send(new SignOutCommand(_jwtToken));
            //await _navigationService.NavigateAsync(nameof(DeviceAuthPage));
        });

    }
}
