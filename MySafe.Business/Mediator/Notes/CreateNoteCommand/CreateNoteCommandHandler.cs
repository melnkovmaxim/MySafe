using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Notes.CreateNoteCommand
{
    public class CreateNoteCommandHandler : RequestHandlerBase<CreateNoteCommand, NoteJsonBody, NoteEntity>
    {
        public CreateNoteCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}