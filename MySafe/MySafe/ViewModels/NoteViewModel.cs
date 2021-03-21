using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MediatR;
using MySafe.Business.Mediator.Notes.NoteListQuery;
using MySafe.Core.Commands;
using MySafe.Core.Entities.Responses;
using MySafe.Core.Models.Responses.Abstractions;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Presentation.Views;
using Prism.Navigation;

namespace MySafe.Presentation.ViewModels
{
    public class NoteViewModel : AuthorizedRefreshViewModel<ResponseList<Note>>
    {
        private readonly IMediator _mediator;
        public ObservableCollection<Note> Notes { get; set; }

        public NoteViewModel(INavigationService navigationService, IMediator mediator) : base(navigationService)
        {
            _mediator = mediator;
            Notes = new ObservableCollection<Note>();
        }

        public AsyncCommand<Note> MoveToNoteEditCommand => _moveToNoteEditCommand ??= new AsyncCommand<Note>(async (note) =>
        {
            var @params = GetItemNaviigationParams(note.Id, note.ClippedContent);
            await _navigationService.NavigateAsync(nameof(NoteEditPage), @params);
        });

        private AsyncCommand<Note> _moveToNoteEditCommand;

        protected override Task<ResponseList<Note>> _refreshTask => _mediator.Send(new NoteListQuery());
        protected override void RefillObservableCollection(ResponseList<Note> mediatorResponse)
        {
            Notes.Clear();
            mediatorResponse.ForEach(Notes.Add);
        }
    }
}
