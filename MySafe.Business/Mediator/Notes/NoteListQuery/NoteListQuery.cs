﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using MySafe.Core.Models.Responses.Abstractions;
using RestSharp;

namespace MySafe.Business.Mediator.Notes.NoteListQuery
{
    /// <summary>
    /// Получение списка заметок
    /// </summary>
    public class NoteListQuery: BearerRequestBase<ResponseList<Note>>
    {
        public override Method RequestMethod => Method.GET;
        public override string RequestResource => "/api/v1/notes";
    }
}
