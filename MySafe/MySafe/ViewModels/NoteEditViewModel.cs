using System.Threading.Tasks;
using MediatR;
using MySafe.Core.Entities.Responses;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Services.Mediator.Notes.NoteInfoQuery;
using Prism.Navigation;

namespace MySafe.Presentation.ViewModels
{
    public class NoteEditViewModel : AuthorizedRefreshViewModel<Note>
    {
        private readonly IMediator _mediator;

        public NoteEditViewModel(INavigationService navigationService, IMediator mediator) : base(navigationService)
        {
            _mediator = mediator;
        }

        public Note Note { get; set; }

        protected override Task<Note> _refreshTask => _mediator.Send(new NoteInfoQuery(_itemId.Value));

        protected override void RefillObservableCollection(Note mediatorResponse)
        {
            Note = mediatorResponse;
        }
    }
}