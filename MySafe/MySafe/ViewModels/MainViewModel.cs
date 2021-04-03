using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Core.Commands;
using MySafe.Core.Entities.Responses;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Presentation.Views;
using MySafe.Services.Mediator.Safe.SafeInfoQuery;
using MySafe.Services.Mediator.Users.SignOutCommand;
using Prism.Navigation;

namespace MySafe.Presentation.ViewModels
{
    public class MainViewModel : AuthorizedRefreshViewModel<Safe>
    {
        private readonly IMapper _mapper;

        private readonly IMediator _mediator;

        private int _maxCapacity;
        private AsyncCommand<Folder> _moveToFolderCommand;
        private AsyncCommand _signOutCommand;
        private int _usedCapacity;

        public MainViewModel(INavigationService navigationService, IMediator mediator, IMapper mapper)
            : base(navigationService)
        {
            _mediator = mediator;
            _mapper = mapper;
            Folders = new ObservableCollection<Folder>();
        }

        public double Progress { get; set; }
        public string SafeSizeInfo { get; set; }

        public ObservableCollection<Folder> Folders { get; set; }

        protected override Task<Safe> _refreshTask => _mediator.Send(new SafeInfoQuery());

        public AsyncCommand<Folder> MoveToFolderCommand =>
            _moveToFolderCommand ??= new AsyncCommand<Folder>(async folder =>
            {
                var @params = GetItemNaviigationParams(folder.Id, folder.Name);
                await _navigationService.NavigateAsync(nameof(FolderPage), @params);
            });

        public AsyncCommand SignOutCommand => _signOutCommand ??= new AsyncCommand(async () =>
        {
            _ = _mediator.Send(new SignOutCommand());
            await _navigationService.NavigateAsync(nameof(DeviceAuthPage));
        });

        protected override void RefillObservableCollection(Safe mediatorResponse)
        {
            //_mapper.Map(Folders, mediatorResponse.Folders);

            _maxCapacity = Convert.ToInt32(Math.Floor(mediatorResponse.Capacity));
            _usedCapacity = Convert.ToInt32(Math.Floor(mediatorResponse.UsedCapacity));

            Progress = Math.Floor((double) _maxCapacity / _usedCapacity) / 100;
            SafeSizeInfo = $"{_usedCapacity}/{_maxCapacity} MB";

            Folders.Clear();
            //queryResponse.Folders.ForEach(Folders.Add);
            mediatorResponse.Folders.ForEach(x =>
            {
                x.Name = x.Name.Split(':').FirstOrDefault();
                Folders.Add(x);
            });
        }
    }
}