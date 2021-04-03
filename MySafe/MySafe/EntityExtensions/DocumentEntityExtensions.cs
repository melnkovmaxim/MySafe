using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Core.Models.Responses;
using MySafe.Presentation.Models;

namespace MySafe.Presentation.EntityExtensions
{
    public static class DocumentEntityExtensions
    {
        public static Document ToDocumentToPresentationModel(this DocumentEntity documentEntity) => 
            Ioc.Resolve<IMapper>().Map<Document>(documentEntity);
    }
}
