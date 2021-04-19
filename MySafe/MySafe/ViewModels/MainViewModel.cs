using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Core.Commands;
using MySafe.Core.Models.Responses;
using MySafe.Domain.Services;
using MySafe.Presentation.Models;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Presentation.Views;
using MySafe.Services.Mediator.Safe.SafeInfoQuery;
using MySafe.Services.Mediator.Users.SignOutCommand;
using Prism.Navigation;

namespace MySafe.Presentation.ViewModels
{
    public class MainViewModel : AuthorizedRefreshViewModel<SafeEntity, Safe>
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly ICalculationSafeCapacityService _calculationSafeCapacityService;
        private readonly IMediator _mediator;

        private AsyncCommand<Folder> _moveToFolderCommand;
        private AsyncCommand _signOutCommand;

        public MainViewModel(
            INavigationService navigationService, 
            IMediator mediator, 
            IMapper mapper, 
            IAuthService authService,
            ICalculationSafeCapacityService calculationSafeCapacityService)
            : base(navigationService, mapper, authService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _authService = authService;
            _calculationSafeCapacityService = calculationSafeCapacityService;
            Folders = new ObservableCollection<Folder>();
        }

        public double Progress { get; set; }
        public string SafeSizeInfo { get; set; }

        public ObservableCollection<Folder> Folders { get; set; }

        protected override Task<SafeEntity> _refreshTask => _mediator.Send(new SafeInfoQuery());

        public AsyncCommand<Folder> MoveToFolderCommand =>
            _moveToFolderCommand ??= new AsyncCommand<Folder>(async folder =>
            {
                var @params = new NavigationParameters() { { nameof(NavigationParameter), new NavigationParameter(folder.Id, folder.Name) }};
                await _navigationService.NavigateAsync(nameof(FolderPage), @params);
            });

        public AsyncCommand SignOutCommand => _signOutCommand ??= new AsyncCommand(async () =>
        {
            await _navigationService.NavigateAsync(nameof(SignInPage));
        });

        protected override void RefillObservableCollection(Safe mediatorEntity)
        {
            var maxCapacity = mediatorEntity.Capacity;
            var usedCapacity = mediatorEntity.UsedCapacity;

            Progress = _calculationSafeCapacityService.GetUsedCapacityInPercents(maxCapacity, usedCapacity) / 100;
            SafeSizeInfo = $"{usedCapacity}/{maxCapacity} MB";

            Folders.Clear();
            mediatorEntity.Folders.ForEach(x =>
            {
                x.Name = x.Name.Split(':').FirstOrDefault();
                Folders.Add(x);
            });
        }
    }
}