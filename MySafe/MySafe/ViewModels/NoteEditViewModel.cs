using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Business.Mediator.Notes.NoteInfoQuery;
using MySafe.Core.Entities.Responses;
using MySafe.Presentation.ViewModels.Abstractions;
using Prism.Navigation;

namespace MySafe.Presentation.ViewModels
{
    public class NoteEditViewModel: AuthorizedRefreshViewModel<Note>
    {
        private readonly IMediator _mediator;
        public Note Note { get; set; }

        public NoteEditViewModel(INavigationService navigationService, IMediator mediator) : base(navigationService)
        {
            _mediator = mediator;
        }

        protected override Task<Note> _refreshTask => _mediator.Send(new NoteInfoQuery(_itemId.Value));
        protected override void RefillObservableCollection(Note mediatorResponse)
        {
            Note = mediatorResponse;
        }
    }
}
