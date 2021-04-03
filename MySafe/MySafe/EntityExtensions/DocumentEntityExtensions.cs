using AutoMapper;
using MySafe.Core.Models.Responses;
using MySafe.Presentation.Models;

namespace MySafe.Presentation.EntityExtensions
{
    public static class DocumentEntityExtensions
    {
        public static Document ToDocumentToPresentationModel(this DocumentEntity documentEntity)
        {
            return Ioc.Resolve<IMapper>().Map<Document>(documentEntity);
        }
    }
}