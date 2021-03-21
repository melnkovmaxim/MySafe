using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Business.Mediator.Notes.CreateNoteCommand
{
    /// <summary>
    /// Создание заметки
    /// </summary>
    public class CreateNoteCommand: BearerRequestBase<Note>
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        public CreateNoteCommand(string content)
        {
            Content = content;
        }

        public override Method RequestMethod => Method.POST;
        public override string RequestResource => "/api/v1/notes";
    }
}
