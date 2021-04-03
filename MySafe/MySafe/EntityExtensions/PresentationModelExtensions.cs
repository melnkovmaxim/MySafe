using AutoMapper;
using MySafe.Core.Entities.Abstractions;
using MySafe.Presentation.Models.Abstractions;

namespace MySafe.Presentation.EntityExtensions
{
    public static class PresentationModelExtensions
    {
        public static IPresentationModel ToPresentationModel(this IEntity entity)
        {
            return Ioc.Resolve<IMapper>().Map<IPresentationModel>(entity);
        }
    }
}