using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Core.Commands;
using MySafe.Core.Entities.Abstractions;
using MySafe.Core.Models.Responses;
using MySafe.Domain.Services;
using MySafe.Presentation.Models;
using MySafe.Presentation.Models.Abstractions;
using MySafe.Presentation.PopupViews;
using MySafe.Presentation.PopupViews.Note;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Presentation.Views;
using MySafe.Services.Mediator.Notes.NoteListQuery;
using MySafe.Services.Mediator.Notes.RemoveNoteCommand;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace MySafe.Presentation.ViewModels
{
    public class NoteViewModel : AuthorizedRefreshViewModel<EntityList<NoteEntity>, PresentationModelList<Note>>
    {
        private readonly IMediator _mediator;

        private AsyncCommand<Note> _moveToNoteEditCommand;
        public AsyncCommand<Note> ShowToolMenuCommand { get; }
        public AsyncCommand AddNoteCommand { get; }

        public NoteViewModel(INavigationService navigationService, IMediator mediator, IMapper mapper, IJwtService jwtService) : base(
            navigationService, mapper, jwtService)
        {
            _mediator = mediator;
            ShowToolMenuCommand = new AsyncCommand<Note>(ShowToolMenuCommandTask);
            AddNoteCommand = new AsyncCommand(AddNoteCommandTask);
            Notes = new ObservableCollection<Note>();
        }

        private async Task AddNoteCommandTask()
        {
            await _navigationService.NavigateAsync(nameof(NoteEditPage));
        }

        private async Task ShowToolMenuCommandTask(Note note)
        {
            const string removeOption = "Удалить";

            var result = await Application.Current.MainPage
                .DisplayActionSheet("", "Отмена", null, removeOption);

            if (result.Contains(removeOption))
            {
                _ = await _mediator.Send(new RemoveNoteCommand(note.Id));
            }
        }

        public ObservableCollection<Note> Notes { get; set; }

        public AsyncCommand<Note> MoveToNoteEditCommand => _moveToNoteEditCommand ??=
            new AsyncCommand<Note>(
                async note =>
                {
                    var @params = new NavigationParameters() { { nameof(NavigationParameter), new NavigationParameter(note.Id, note.ClippedContent) }};
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