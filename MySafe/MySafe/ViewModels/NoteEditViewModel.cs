using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Core.Models.Responses;
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

        public NoteEditViewModel(INavigationService navigationService, IMediator mediator, IMapper mapper) : base(
            navigationService, mapper)
        {
            _mediator = mediator;
        }

        public Note Note { get; set; }

        protected override Task<NoteEntity> _refreshTask => _itemId != null ? _mediator.Send(new NoteInfoQuery(_itemId.Value)) : Task.FromResult(new NoteEntity());

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
            if (_itemId != null)
            {
                await _mediator.Send(new ChangeNoteCommand(Note.Id, Note.Content));
            }
            else
            {
                await _mediator.Send(new CreateNoteCommand(Note.Content));
            }
        }
    }
}