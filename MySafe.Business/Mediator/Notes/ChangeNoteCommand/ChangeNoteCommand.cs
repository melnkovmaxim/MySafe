using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Business.Mediator.Notes.ChangeNoteCommand
{
    /// <summary>
    /// Изменение заметки
    /// </summary>
    public class ChangeNoteCommand: BearerRequestBase<Note>
    {
        [JsonIgnore]
        public int NoteId { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        public override Method RequestMethod => Method.PUT;
        public override string RequestResource => $"/api/v1/notes/{NoteId}";
    }
}
