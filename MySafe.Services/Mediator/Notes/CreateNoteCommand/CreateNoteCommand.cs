using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Notes.CreateNoteCommand
{
    /// <summary>
    ///     Создание заметки
    /// </summary>
    public class CreateNoteCommand : BearerRequestBase<NoteEntity>
    {
        public CreateNoteCommand(string content)
        {
            Content = content;
        }

        public string Content { get; set; }

        public override Method RequestMethod => Method.POST;
        public override string RequestResource => "/api/v1/notes";
    }
}