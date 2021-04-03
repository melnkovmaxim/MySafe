using AutoMapper;
using MySafe.Core.Models.Responses;
using MySafe.Presentation.Models;

namespace MySafe.Presentation.EntityExtensions
{
    public static class SafeEntityExtensions
    {
        public static Safe ToSafePresentationModel(this SafeEntity safeEntity)
        {
            return Ioc.Resolve<IMapper>().Map<Safe>(safeEntity);
        }
    }
}