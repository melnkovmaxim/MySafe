using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Notes.RemoveNoteCommand
{
    public class RemoveNoteCommandHandler : RequestHandlerBase<RemoveNoteCommand, NoteJsonBody, NoteEntity>
    {
        public RemoveNoteCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}