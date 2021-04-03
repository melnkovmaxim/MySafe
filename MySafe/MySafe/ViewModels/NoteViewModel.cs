using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Core.Commands;
using MySafe.Core.Entities.Abstractions;
using MySafe.Core.Models.Responses;
using MySafe.Presentation.Models;
using MySafe.Presentation.Models.Abstractions;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Presentation.Views;
using MySafe.Services.Mediator.Notes.NoteListQuery;
using Prism.Navigation;
using Xamarin.Forms;

namespace MySafe.Presentation.ViewModels
{
    public class NoteViewModel : AuthorizedRefreshViewModel<EntityList<NoteEntity>, PresentationModelList<Note>>
    {
        private readonly IMediator _mediator;

        private AsyncCommand<Note> _moveToNoteEditCommand;
        public AsyncCommand<Note> ShowToolMenuCommand { get; }

        public NoteViewModel(INavigationService navigationService, IMediator mediator, IMapper mapper) : base(
            navigationService, mapper)
        {
            _mediator = mediator;
            ShowToolMenuCommand = new AsyncCommand<Note>(ShowToolMenuCommandTask);
            Notes = new ObservableCollection<Note>();
        }

        private async Task ShowToolMenuCommandTask(Note note)
        {
            const string editOption = "Изменить название";
            const string removeOption = "Удалить";
            var str = "hello";
            var result = await Application.Current.MainPage
                .DisplayActionSheet("Выбрать", "Отмена", str, editOption, removeOption);

            if (result.Contains(editOption))
            {
            }
            if (result.Contains(removeOption)) {}
        }

        public ObservableCollection<Note> Notes { get; set; }

        public AsyncCommand<Note> MoveToNoteEditCommand => _moveToNoteEditCommand ??=
            new AsyncCommand<Note>(
                async note =>
                {
                    var @params = GetItemNaviigationParams(note.Id, note.ClippedContent);
                    await _navigationService.NavigateAsync(nameof(NoteEditPage), @params);
                });

        protected override Task<EntityList<NoteEntity>> _refreshTask => _mediator.Send(new NoteListQuery());

        protected override void RefillObservableCollection(PresentationModelList<Note> noteList)
        {
            Notes.Clear();
            noteList.ForEach(Notes.Add);
        }
    }
}