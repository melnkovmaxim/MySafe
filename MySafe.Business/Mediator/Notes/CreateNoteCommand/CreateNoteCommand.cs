using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using Newtonsoft.Json;
using RestSharp;

namespace MySafe.Services.Mediator.Notes.CreateNoteCommand
{
    /// <summary>
    ///     Создание заметки
    /// </summary>
    public class CreateNoteCommand : BearerRequestBase<Note>
    {
        public CreateNoteCommand(string content)
        {
            Content = content;
        }

        [JsonProperty("content")] public string Content { get; set; }

        public override Method RequestMethod => Method.POST;
        public override string RequestResource => "/api/v1/notes";
    }
}