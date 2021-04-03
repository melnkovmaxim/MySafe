using MySafe.Core.Entities.Responses;
using MySafe.Core.Models.Responses.Abstractions;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Notes.NoteListQuery
{
    public class NoteListQueryHandler : RequestHandlerBase<NoteListQuery, ResponseList<Note>>
    {
        public NoteListQueryHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}