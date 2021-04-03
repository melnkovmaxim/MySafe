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

namespace MySafe.Presentation.ViewModels
{
    public class NoteViewModel : AuthorizedRefreshViewModel<EntityList<NoteEntity>, PresentationModelList<Note>>
    {
        private readonly IMediator _mediator;

        private AsyncCommand<Note> _moveToNoteEditCommand;

        public NoteViewModel(INavigationService navigationService, IMediator mediator, IMapper mapper) : base(
            navigationService, mapper)
        {
            _mediator = mediator;
            Notes = new ObservableCollection<Note>();
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