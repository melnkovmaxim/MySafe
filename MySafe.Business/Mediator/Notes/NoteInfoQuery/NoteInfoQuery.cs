using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Notes.NoteInfoQuery
{
    /// <summary>
    ///     Получение заметки
    /// </summary>
    public class NoteInfoQuery : BearerRequestBase<NoteEntity>
    {
        public NoteInfoQuery(int noteId)
        {
            NoteId = noteId;
        }

        public int NoteId { get; set; }

        public override Method RequestMethod => Method.GET;
        public override string RequestResource => $"/api/v1/notes/{NoteId}";
    }
}