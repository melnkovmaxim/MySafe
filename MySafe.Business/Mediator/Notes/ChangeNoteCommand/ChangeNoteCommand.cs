using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Services.Mediator.Notes.ChangeNoteCommand
{
    /// <summary>
    ///     Изменение заметки
    /// </summary>
    public class ChangeNoteCommand : BearerRequestBase<Note>
    {
        [JsonIgnore] public int NoteId { get; set; }

        [JsonProperty("content")] public string Content { get; set; }

        public override Method RequestMethod => Method.PUT;
        public override string RequestResource => $"/api/v1/notes/{NoteId}";
    }
}