using AutoMapper;
using MySafe.Core.Entities.Abstractions;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Notes.NoteListQuery
{
    public class NoteListQueryHandler : RequestHandlerBase<NoteListQuery, NoteJsonBody, EntityList<NoteEntity>>
    {
        public NoteListQueryHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}