using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Business.Mediator.Notes.RemoveNoteCommand
{
    /// <summary>
    /// Удаление заметки
    /// </summary>
    public class RemoveNoteCommand: BearerRequestBase<Note>
    {
        [JsonIgnore]
        public int NoteId { get; set; }

        public RemoveNoteCommand(int noteId)
        {
            NoteId = noteId;
        }

        public override Method RequestMethod => Method.DELETE;
        public override string RequestResource => $"/api/v1/notes/{NoteId}";
    }
}
