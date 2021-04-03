using MySafe.Core.Entities.Responses;
using MySafe.Core.Models.Responses.Abstractions;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Notes.NoteListQuery
{
    /// <summary>
    ///     Получение списка заметок
    /// </summary>
    public class NoteListQuery : BearerRequestBase<ResponseList<Note>>
    {
        public override Method RequestMethod => Method.GET;
        public override string RequestResource => "/api/v1/notes";
    }
}