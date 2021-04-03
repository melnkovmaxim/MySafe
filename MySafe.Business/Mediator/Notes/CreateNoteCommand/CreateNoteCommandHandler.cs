using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Notes.CreateNoteCommand
{
    public class CreateNoteCommandHandler : RequestHandlerBase<CreateNoteCommand, Note>
    {
        public CreateNoteCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}