using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MediatR;
using MySafe.Business.Mediator.Notes.NoteListQuery;
using MySafe.Core.Entities.Responses;
using MySafe.Presentation.ViewModels.Abstractions;
using Prism.Navigation;

namespace MySafe.Presentation.ViewModels
{
    public class NoteViewModel : AuthorizedRefreshViewModel<Note>
    {
        private readonly IMediator _mediator;
        public ObservableCollection<Note> Notes { get; set; }

        public NoteViewModel(INavigationService navigationService, IMediator mediator) : base(navigationService)
        {
            _mediator = mediator;
        }

        protected override Task<Note> _refreshTask => _mediator.Send(new NoteListQuery());
        protected override void RefillObservableCollection(Note mediatorResponse)
        {
            /// Нужен акк Влада чтоб проверить резалтсет
            Notes.Clear();
        }
    }
}
