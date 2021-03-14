using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using MySafe.Core.Models.Responses.Abstractions;
using RestSharp;

namespace MySafe.Business.Mediator.Notes.NoteListQuery
{
    public class NoteListQueryHandler: RequestHandlerBase<NoteListQuery, ResponseList<Note>>
    {
        public NoteListQueryHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}
