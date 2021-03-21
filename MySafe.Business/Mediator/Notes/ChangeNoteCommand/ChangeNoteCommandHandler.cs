﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using RestSharp;

namespace MySafe.Business.Mediator.Notes.ChangeNoteCommand
{
    public class ChangeNoteCommandHandler: RequestHandlerBase<ChangeNoteCommand, Note>
    {
        public ChangeNoteCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}