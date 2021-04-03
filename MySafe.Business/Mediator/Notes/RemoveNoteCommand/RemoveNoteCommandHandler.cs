using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Notes.RemoveNoteCommand
{
    public class RemoveNoteCommandHandler : RequestHandlerBase<RemoveNoteCommand, Note>
    {
        public RemoveNoteCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}