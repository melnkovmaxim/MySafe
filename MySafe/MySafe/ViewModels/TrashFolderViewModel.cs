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
        private AsyncCommand _clearTrashCommand;
        private AsyncCommand<Trash> _showItemActionMenuCommand;

        public TrashFolderViewModel(INavigationService navigationService, IMediator mediator, IMapper mapper, IAuthService authService)
            : base(navigationService, mapper, authService)
        {
            _mediator = mediator;
            _mapper = mapper;

            TrashItems = new ObservableCollection<Trash>();
        }

        public string FolderName => "Корзина";
        public ObservableCollection<Trash> TrashItems { get; set; }

        public AsyncCommand ClearTrashCommand => _clearTrashCommand ??= new AsyncCommand(async () =>
        {
            var response = await _mediator.Send(new ClearTrashCommand());

            if (response.HasError)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка",
                    "Не получилось загрузить файл, что-то пошло не так... ", "Ok");
                return;
            }

            TrashItems.Clear();
        });

        public AsyncCommand<Trash> ShowItemActionMenuCommand =>
            _showItemActionMenuCommand ??= new AsyncCommand<Trash>(async trashItem =>
            {
                const string restore = "Восстановить";
                const string destroy = "Уничтожить";
                const string cancelButtonName = "Отмена";

                var result = await Application.Current.MainPage
                    .DisplayActionSheet("Опции", cancelButtonName, null, restore, destroy);

                if (result == cancelButtonName) return;

                IEntity response = null;

                if (result.Contains(restore)) response = await RestoreTrashItem(trashItem);

                if (result.Contains(destroy)) response = await DestroyTrashItem(trashItem);

                if (response?.HasError != false)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка",
                        "Не получилось загрузить файл, что-то пошло не так... ", "Ok");
                    return;
                }

                TrashItems.Remove(trashItem);
            });

        protected override Task<EntityList<TrashEntity>> _refreshTask => _mediator.Send(new TrashContentQuery());

        private async Task<IEntity> DestroyTrashItem(Trash trashItem)
        {
            if (trashItem.IsFolder)
                return await _mediator.Send(new DestroyTrashDocumentCommand(trashItem.Id)).ConfigureAwait(false);

            if (trashItem.IsImage)
                return await _mediator.Send(new DestroyTrashImageCommand(trashItem.Id)).ConfigureAwait(false);
            return await _mediator.Send(new DestroyTrashSheetCommand(trashItem.Id)).ConfigureAwait(false);
        }

        private async Task<IEntity> RestoreTrashItem(Trash trashItem)
        {
            if (trashItem.IsFolder)
                return await _mediator.Send(new DestroyTrashDocumentCommand(trashItem.Id)).ConfigureAwait(false);

            if (trashItem.IsImage)
                return await _mediator.Send(new RestoreTrashImageCommand(trashItem.Id)).ConfigureAwait(false);
            return await _mediator.Send(new RestoreTrashSheetCommand(trashItem.Id)).ConfigureAwait(false);
        }

        protected override void RefillObservableCollection(PresentationModelList<Trash> mediatorResponse)
        {
            var trashes = _mapper.Map<List<Trash>>(mediatorResponse);
            TrashItems.Clear();
            trashes.ForEach(TrashItems.Add);
        }
    }
}