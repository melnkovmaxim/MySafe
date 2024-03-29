﻿using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Notes.ChangeNoteCommand
{
    /// <summary>
    ///     Изменение заметки
    /// </summary>
    public class ChangeNoteCommand : BearerRequestBase<NoteEntity>
    {
        public int NoteId { get; set; }

        public string Content { get; set; }

        public ChangeNoteCommand(int noteId, string content)
        {
            NoteId = noteId;
            Content = content;
        }

        public override Method RequestMethod => Method.PUT;
        public override string RequestResource => $"/api/v1/notes/{NoteId}";
    }
}