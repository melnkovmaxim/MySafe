﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using RestSharp;

namespace MySafe.Business.Mediator.Sheets.SheetPdfFormatQuery
{
    public class FilePdfFormatQueryHandler: RequestHandlerBase<SheetPdfFormatQuery, Sheet>
    {
        public FilePdfFormatQueryHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}
