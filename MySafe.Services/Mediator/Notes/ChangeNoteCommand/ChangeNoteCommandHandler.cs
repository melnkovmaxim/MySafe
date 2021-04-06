using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Notes.ChangeNoteCommand
{
    public class ChangeNoteCommandHandler : RequestHandlerBase<ChangeNoteCommand, NoteJsonBody, NoteEntity>
    {
        public ChangeNoteCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}