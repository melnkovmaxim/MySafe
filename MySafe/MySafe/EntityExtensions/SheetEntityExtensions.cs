using AutoMapper;
using MySafe.Core.Models.Responses;
using MySafe.Presentation.Models;

namespace MySafe.Presentation.EntityExtensions
{
    public static class SheetEntityExtensions
    {
        public static Sheet ToSheetPresentationModel(this SheetEntity sheetEntity)
        {
            return Ioc.Resolve<IMapper>().Map<Sheet>(sheetEntity);
        }
    }
}