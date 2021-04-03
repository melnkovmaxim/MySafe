using MySafe.Core.Entities.Abstractions;
using MySafe.Core.Models;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Notes.NoteListQuery
{
    /// <summary>
    ///     Получение списка заметок
    /// </summary>
    public class NoteListQuery : BearerRequestBase<EntityList<NoteEntity>>
    {
        public override Method RequestMethod => Method.GET;
        public override string RequestResource => "/api/v1/notes";
    }
}