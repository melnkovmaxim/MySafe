using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Core.Models;
using MySafe.Core.Models.Responses;
using MySafe.Presentation.Models;

namespace MySafe.Presentation.EntityExtensions
{
    public static class SheetEntityExtensions
    {
        public static Sheet ToSheetPresentationModel(this SheetEntity sheetEntity) =>
            Ioc.Resolve<IMapper>().Map<Sheet>(sheetEntity);
    }
}
