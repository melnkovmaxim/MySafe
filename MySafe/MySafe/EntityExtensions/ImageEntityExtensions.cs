using AutoMapper;
using MySafe.Core.Models.Responses;
using MySafe.Presentation.Models;

namespace MySafe.Presentation.EntityExtensions
{
    public static class ImageEntityExtensions
    {
        public static Image ToImagePresentationModel(this ImageEntity imageEntity)
        {
            return Ioc.Resolve<IMapper>().Map<Image>(imageEntity);
        }
    }
}