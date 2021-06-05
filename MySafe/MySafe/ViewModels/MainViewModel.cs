using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Core.Commands;
using MySafe.Core.Interfaces.Services;
using MySafe.Core.Models.Responses;
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

        public AsyncCommand<Folder> MoveToFolderCommand { get; }
        public AsyncCommand SignOutCommand { get; }

        public ObservableCollection<Folder> Folders { get; set; }
        public double Progress { get; set; }
        public string SafeSizeInfo { get; set; }

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

            MoveToFolderCommand = new AsyncCommand<Folder>(MoveToFolderCommandTask);
            SignOutCommand = new AsyncCommand(SignOutCommandTask);

            Folders = new ObservableCollection<Folder>(Enumerable.Range(1, 6).Select(x => new Folder()));
        }

        protected override Task<SafeEntity> _refreshTask => _mediator.Send(new SafeInfoQuery());

        public Task MoveToFolderCommandTask(Folder folder)
        {
            var @params = new NavigationParameters()
            {
                { nameof(NavigationParameter), new NavigationParameter(folder.Id, folder.Name) }
            };

            return _navigationService.NavigateAsync(nameof(FolderPage), @params);
        }

        public Task SignOutCommandTask()
        {
            return _navigationService.NavigateAsync(nameof(SignInPage));
        }

        public double ProgressforName(double progressSend) 
        {
            if (progressSend >= 10)
                return (int)Math.Round(progressSend, 0);
            else
                return Math.Round(progressSend, 2);
        }

        protected override void RefillObservableCollection(Safe mediatorEntity)
        {
            var maxCapacity = mediatorEntity.Capacity;
            var usedCapacity = mediatorEntity.UsedCapacity;

            Progress = _calculationSafeCapacityService.GetUsedCapacityInPercents(maxCapacity, usedCapacity) / 100;

            //var percentCapacity = Math.Round(Progress, 2);
            var percentCapacity = ProgressforName(Progress);
            SafeSizeInfo = $"{usedCapacity}/{maxCapacity} MB {percentCapacity}%";

            Folders.Clear();

            mediatorEntity.Folders.ForEach(folder =>
            {
                folder.Name = GetClearFolderName(folder.Name);
                Folders.Add(folder);
            });
        }

        private string GetClearFolderName(string folderName)
        {
            var clearName = folderName
                .Split(':')
                .First()
                .Split(',')
                .First();

            return clearName + ":";
        }
    }

    public class progressSend
    {
    }
}