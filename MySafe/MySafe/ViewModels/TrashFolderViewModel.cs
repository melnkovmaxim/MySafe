using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Core.Commands;
using MySafe.Core.Entities.Abstractions;
using MySafe.Core.Interfaces.Services;
using MySafe.Core.Models.Responses;
using MySafe.Presentation.Models;
using MySafe.Presentation.Models.Abstractions;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Services.Mediator.Documents.DestroyTrashDocumentCommand;
using MySafe.Services.Mediator.Images.DestroyTrashImageCommand;
using MySafe.Services.Mediator.Images.RestoreTrashImageCommand;
using MySafe.Services.Mediator.Sheets.DestroyTrashSheetCommand;
using MySafe.Services.Mediator.Sheets.RestoreTrashSheetCommand;
using MySafe.Services.Mediator.Trash.ClearTrashCommand;
using MySafe.Services.Mediator.Trash.TrashContentQuery;
using Prism.Navigation;
using Xamarin.Forms;

namespace MySafe.Presentation.ViewModels
{
    public class
        TrashFolderViewModel : AuthorizedRefreshViewModel<EntityList<TrashEntity>, PresentationModelList<Trash>>
    {
        private readonly IMapper _mapper;

        private readonly IMediator _mediator;

        public AsyncCommand ClearTrashCommand { get; }
        public AsyncCommand<Trash> ShowItemActionMenuCommand { get; }
        public ObservableCollection<Trash> TrashItems { get; set; }
        public string FolderName => "Корзина";

        public TrashFolderViewModel(INavigationService navigationService, IMediator mediator, IMapper mapper, IAuthService authService)
            : base(navigationService, mapper, authService)
        {
            _mediator = mediator;
            _mapper = mapper;

            TrashItems = new ObservableCollection<Trash>();
        }


        public async Task ClearTrashCommandTask()
        {
            var response = await _mediator.Send(new ClearTrashCommand());

            if (response.HasError)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Не получилось загрузить файл, что-то пошло не так... ", "Ok");
                
                return;
            }

            TrashItems.Clear();
        }

        public async Task ShowItemActionMenuCommandTask(Trash trash)
        {
            const string restoreName = "Восстановить";
            const string destroyName = "Уничтожить";
            const string cancelButtonName = "Отмена";

            var result = await Application.Current.MainPage
                .DisplayActionSheet("Опции", cancelButtonName, null, restoreName, destroyName);

            if (result == cancelButtonName) return;

            IEntity response = null;

            if (result.Contains(restoreName))
            {
                response = await RestoreTrashItem(trash);
            }

            if (result.Contains(destroyName))
            {
                response = await DestroyTrashItem(trash);
            }

            if (response?.HasError != false)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка","Не получилось загрузить файл, что-то пошло не так... ", "Ok");
                
                return;
            }

            TrashItems.Remove(trash);
        }

        protected override Task<EntityList<TrashEntity>> _refreshTask => _mediator.Send(new TrashContentQuery());

        private async Task<IEntity> DestroyTrashItem(Trash trashItem)
        {
            if (trashItem.IsFolder)
            {
                return await _mediator.Send(new DestroyTrashDocumentCommand(trashItem.Id));
            }

            if (trashItem.IsImage)
            {
                return await _mediator.Send(new DestroyTrashImageCommand(trashItem.Id));
            }

            return await _mediator.Send(new DestroyTrashSheetCommand(trashItem.Id));
        }

        private async Task<IEntity> RestoreTrashItem(Trash trashItem)
        {
            if (trashItem.IsFolder)
            {
                return await _mediator.Send(new DestroyTrashDocumentCommand(trashItem.Id));
            }

            if (trashItem.IsImage)
            {
                return await _mediator.Send(new RestoreTrashImageCommand(trashItem.Id));
            }

            return await _mediator.Send(new RestoreTrashSheetCommand(trashItem.Id));
        }

        protected override void RefillObservableCollection(PresentationModelList<Trash> mediatorResponse)
        {
            var trashes = _mapper.Map<List<Trash>>(mediatorResponse);
            TrashItems.Clear();
            trashes.ForEach(TrashItems.Add);
        }
    }
}