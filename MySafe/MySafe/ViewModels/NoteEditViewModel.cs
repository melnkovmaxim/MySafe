using System.Threading.Tasks;
using AutoMapper;
using MediatR;
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
        }

        public Note Note { get; set; }

        protected override Task<NoteEntity> _refreshTask => _isNewNote ? _mediator.Send(new NoteInfoQuery(_navigationParameter.ChildId)) : Task.FromResult(new NoteEntity());

        protected override void RefillObservableCollection(Note note)
        {
            Note = note;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            CreateOrUpdateNote();
            base.OnNavigatedFrom(parameters);
        }

        private async void CreateOrUpdateNote()
        {
            if (_isNewNote)
            {
                await _mediator.Send(new CreateNoteCommand(Note.Content));
            }
            else
            {
                await _mediator.Send(new ChangeNoteCommand(Note.Id, Note.Content));
            }
        }
    }
}