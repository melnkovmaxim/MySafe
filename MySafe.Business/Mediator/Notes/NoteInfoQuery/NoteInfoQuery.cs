using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Services.Mediator.Notes.NoteInfoQuery
{
    /// <summary>
    ///     Получение заметки
    /// </summary>
    public class NoteInfoQuery : BearerRequestBase<Note>
    {
        public NoteInfoQuery(int noteId)
        {
            NoteId = noteId;
        }

        [JsonIgnore] public int NoteId { get; set; }

        public override Method RequestMethod => Method.GET;
        public override string RequestResource => $"/api/v1/notes/{NoteId}";
    }
}