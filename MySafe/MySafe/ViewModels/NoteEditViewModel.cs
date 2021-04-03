using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Core.Models;
using MySafe.Core.Models.Responses;
using MySafe.Presentation.Models;
using MySafe.Presentation.ViewModels.Abstractions;
using MySafe.Services.Mediator.Notes.NoteInfoQuery;
using Prism.Navigation;

namespace MySafe.Presentation.ViewModels
{
    public class NoteEditViewModel : AuthorizedRefreshViewModel<NoteEntity, Note>
    {
        private readonly IMediator _mediator;

        public NoteEditViewModel(INavigationService navigationService, IMediator mediator, IMapper mapper) : base(navigationService, mapper)
        {
            _mediator = mediator;
        }

        public Note Note { get; set; }

        protected override Task<NoteEntity> _refreshTask => _mediator.Send(new NoteInfoQuery(_itemId.Value));

        protected override void RefillObservableCollection(Note note)
        {
            Note = note;
        }
    }
}