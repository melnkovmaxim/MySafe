using System.Collections.Generic;
using MediatR;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MySafe.Business.Mediator.Documents.RemoveFromTrash;
using MySafe.Business.Mediator.Documents.RestoreFromTrash;
using MySafe.Business.Mediator.Images.RemoveFromTrash;
using MySafe.Business.Mediator.Images.RestoreFromTrash;
using MySafe.Business.Mediator.Sheets.RemoveFromTrash;
using MySafe.Business.Mediator.Sheets.RestoreFromTrash;
using MySafe.Business.Mediator.Trash.ClearTrash;
using MySafe.Business.Mediator.Trash.GetTrashInfo;
using MySafe.Core.Commands;
using MySafe.Core.Entities.Responses;
using MySafe.Core.Entities.Responses.Abstractions;
using MySafe.Presentation.Models;
using MySafe.Presentation.ViewModels.Abstractions;
using Xamarin.Forms;
using IMapper = AutoMapper.IMapper;

namespace MySafe.Presentation.ViewModels
{
    public class TrashFolderViewModel: AuthorizedViewModelBase
    {
        public string FolderName => "Корзина";
        public ObservableCollection<Trash> TrashItems { get; set; }

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private AsyncCommand _clearTrashCommand;
        private AsyncCommand<Trash> _showItemActionMenuCommand;

        public TrashFolderViewModel(INavigationService navigationService, IMediator mediator, IMapper mapper) 
            : base(navigationService)
        {
            _mediator = mediator;
            _mapper = mapper;

            TrashItems = new ObservableCollection<Trash>();
        }


        protected override async Task ActionAfterLoadPage()
        {
            var queryResponse = await _mediator.Send(new TrashInfoQuery(_jwtToken));

            if (queryResponse == null)
            {
                return;
            }

            var trashes = _mapper.Map<List<Trash>>(queryResponse);
            TrashItems.Clear();
            trashes.ForEach(TrashItems.Add);
        }

        public AsyncCommand ClearTrashCommand => _clearTrashCommand ??= new AsyncCommand(async () =>
        {
            var response = await _mediator.Send(new ClearTrashCommand(_jwtToken));

            if (response.HasError)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Не получилось загрузить файл, что-то пошло не так... ", "Ok");
                return;
            }

            TrashItems.Clear();
        });

        public AsyncCommand<Trash> ShowItemActionMenuCommand => 
            _showItemActionMenuCommand ??= new AsyncCommand<Trash>(async (trashItem) =>
        {
            const string restore = "Восстановить";
            const string destroy = "Уничтожить";
            var result = await Application.Current.MainPage
                .DisplayActionSheet ("Опции", "Отмена", null, restore, destroy);

            if (string.IsNullOrEmpty(result)) return;

            BaseResponse response = null;

            if (result.Contains(restore))
            {
                response = await RestoreTrashItem(trashItem);
            }
            
            if (result.Contains(destroy))
            {
                response = await DestroyTrashItem(trashItem);
            }

            if (response?.HasError != false)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Не получилось загрузить файл, что-то пошло не так... ", "Ok");
                return;
            }

            TrashItems.Remove(trashItem);
        });

        private async Task<BaseResponse> DestroyTrashItem(Trash trashItem)
        {
            if (trashItem.IsFolder)
            {
                return await _mediator.Send(new RemoveDocFromTrashCommand(_jwtToken, trashItem.Id)).ConfigureAwait(false);
            }
            
            if (trashItem.IsImage)
            {
                return await _mediator.Send(new RemoveImgFromTrashCommand(_jwtToken, trashItem.Id)).ConfigureAwait(false);
            }
            else
            {
                return await _mediator.Send(new RemoveFileFromTrashCommand(_jwtToken, trashItem.Id)).ConfigureAwait(false);
            }
        }

        private async Task<BaseResponse> RestoreTrashItem(Trash trashItem)
        {
            if (trashItem.IsFolder)
            {
                return await _mediator.Send(new RestoreDocFromTrashCommand(_jwtToken, trashItem.Id)).ConfigureAwait(false);
            }

            if (trashItem.IsImage)
            {
                return await _mediator.Send(new RestoreImgFromTrashCommand(_jwtToken, trashItem.Id)).ConfigureAwait(false);
            }
            else
            {
                return await _mediator.Send(new RestoreFileFromTrashCommand(_jwtToken, trashItem.Id)).ConfigureAwait(false);
            }
        }
    }
}
