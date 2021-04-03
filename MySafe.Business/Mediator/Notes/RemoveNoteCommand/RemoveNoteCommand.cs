using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Services.Mediator.Notes.RemoveNoteCommand
{
    /// <summary>
    ///     Удаление заметки
    /// </summary>
    public class RemoveNoteCommand : BearerRequestBase<NoteEntity>
    {
        public RemoveNoteCommand(int noteId)
        {
            NoteId = noteId;
        }

        public int NoteId { get; set; }

        public override Method RequestMethod => Method.DELETE;
        public override string RequestResource => $"/api/v1/notes/{NoteId}";
    }
}