using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Notes.NoteInfoQuery
{
    public class NoteInfoQueryHandler : RequestHandlerBase<NoteInfoQuery, NoteJsonBody, NoteEntity>
    {
        public NoteInfoQueryHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}