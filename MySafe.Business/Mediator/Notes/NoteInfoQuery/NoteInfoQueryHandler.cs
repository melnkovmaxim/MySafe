using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Notes.NoteInfoQuery
{
    public class NoteInfoQueryHandler : RequestHandlerBase<NoteInfoQuery, Note>
    {
        public NoteInfoQueryHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}