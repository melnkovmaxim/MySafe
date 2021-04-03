using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Notes.ChangeNoteCommand
{
    public class ChangeNoteCommandHandler : RequestHandlerBase<ChangeNoteCommand, Note>
    {
        public ChangeNoteCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}