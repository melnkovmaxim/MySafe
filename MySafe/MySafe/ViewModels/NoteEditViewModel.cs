using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Core.Commands;
using MySafe.Core.Models.Responses;
using MySafe.Domain.Services;
using MySafe.Presentation.Models;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Services.Mediator.Notes.ChangeNoteCommand;
using MySafe.Services.Mediator.Notes.CreateNoteCommand;
using MySafe.Services.Mediator.Notes.NoteInfoQuery;
using Prism.AppModel;
using Prism.Navigation;
using Prism.Navigation.Xaml;

namespace MySafe.Presentation.ViewModels
{
    public class NoteEditViewModel : AuthorizedRefreshViewModel<NoteEntity, Note>, INavigatedAware
    {
        private readonly IMediator _mediator;
        private bool _isNewNote => _navigationParameter == null;

        public NoteEditViewModel(INavigationService navigationService, IMediator mediator, IMapper mapper, IAuthService authService) 
            : base(navigationService, mapper, authService)
        {
            _mediator = mediator;
            SaveNoteCommand = new AsyncCommand(SaveNoteCommandTask);
        }

        public Note Note { get; set; }
        public AsyncCommand SaveNoteCommand { get; }

        protected override Task<NoteEntity> _refreshTask => _isNewNote ? Task.FromResult(new NoteEntity()) : _mediator.Send(new NoteInfoQuery(_navigationParameter.ChildId));

        protected override void RefillObservableCollection(Note note)
        {
            Note = note;
        }

        private Task SaveNoteCommandTask()
        {
            if (_isNewNote)
            {
                return _mediator.Send(new CreateNoteCommand(Note.Content));
            }
            else
            {
                return _mediator.Send(new ChangeNoteCommand(Note.Id, Note.Content));
            }
        }
    }
}