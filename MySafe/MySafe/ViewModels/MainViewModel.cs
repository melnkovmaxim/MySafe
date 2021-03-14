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
using AutoMapper;
using MySafe.Business.Mediator.Safe.SafeInfoQuery;
using MySafe.Business.Mediator.Users.SignOutCommand;
using MySafe.Core.Entities.Responses.Abstractions;
using Xamarin.Forms.Internals;

namespace MySafe.Presentation.ViewModels
{
    public class MainViewModel : AuthorizedRefreshViewModel<Safe>
    {
        public double Progress { get; set; }
        public string SafeSizeInfo { get; set; }

        private int _maxCapacity;
        private int _usedCapacity;

        public ObservableCollection<Folder> Folders { get; set; }

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private AsyncCommand _signOutCommand;
        private AsyncCommand<Folder> _moveToFolderCommand;

        public MainViewModel(INavigationService navigationService, IMediator mediator, IMapper mapper)
            : base(navigationService)
        {
            _mediator = mediator;
            _mapper = mapper;
            Folders = new ObservableCollection<Folder>();
        }

        protected override Task<Safe> _refreshTask => _mediator.Send(new SafeInfoQuery());

        protected override void RefillObservableCollection(Safe mediatorResponse)
        {
            //_mapper.Map(Folders, mediatorResponse.Folders);

            _maxCapacity = Convert.ToInt32(Math.Floor(mediatorResponse.Capacity));
            _usedCapacity = Convert.ToInt32(Math.Floor(mediatorResponse.UsedCapacity));

            Progress = Math.Floor((double)_maxCapacity / _usedCapacity) / 100;
            SafeSizeInfo = $"{_usedCapacity}/{_maxCapacity} MB";

            Folders.Clear();
            //queryResponse.Folders.ForEach(Folders.Add);
            mediatorResponse.Folders.ForEach(x =>
            {
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
            _ = _mediator.Send(new SignOutCommand());
            await _navigationService.NavigateAsync(nameof(DeviceAuthPage));
        });

    }
}
