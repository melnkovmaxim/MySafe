using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Mediator.Documents.RemoveFromTrash;
using MySafe.Mediator.Documents.RestoreFromTrash;
using MySafe.Mediator.Folders.GetFolderInfo;
using MySafe.Mediator.Images.RemoveFromTrash;
using MySafe.Mediator.Images.RestoreFromTrash;
using MySafe.Mediator.Safe.SafeInfo;
using MySafe.Mediator.Sheets.RemoveFromTrash;
using MySafe.Mediator.Sheets.RestoreFromTrash;
using MySafe.Mediator.Trash.ClearTrash;
using MySafe.Mediator.Trash.GetTrashInfo;
using MySafe.Models.Responses;
using MySafe.ViewModels.Abstractions;
using MySafe.Views;
using NetStandardCommands;
using Prism.Navigation;
using Xamarin.Forms;

namespace MySafe.ViewModels
{
    public class TrashFolderViewModel: AuthorizedViewModelBase
    {
        public string FolderName => "Корзина";
        public ObservableCollection<TrashResponse> TrashItems { get; set; }

        private readonly IMediator _mediator;
        private AsyncCommand _clearTrashCommand;
        private AsyncCommand<TrashResponse> _showItemActionMenuCommand;

        public TrashFolderViewModel(INavigationService navigationService, IMediator mediator) 
            : base(navigationService)
        {
            _mediator = mediator;

            TrashItems = new ObservableCollection<TrashResponse>();
        }


        protected override async void ActionAfterLoadPage()
        {
            var queryResponse = await _mediator.Send(new TrashInfoQuery(_jwtToken));

            if (queryResponse == null)
            {
                return;
            }

            TrashItems.Clear();
            queryResponse.ForEach(TrashItems.Add);
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

        public AsyncCommand<TrashResponse> ShowItemActionMenuCommand => 
            _showItemActionMenuCommand ??= new AsyncCommand<TrashResponse>(async (trashItem) =>
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

        private async Task<BaseResponse> DestroyTrashItem(TrashResponse trashItem)
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

        private async Task<BaseResponse> RestoreTrashItem(TrashResponse trashItem)
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
