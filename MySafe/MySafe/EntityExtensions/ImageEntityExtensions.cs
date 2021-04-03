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
    public static class ImageEntityExtensions
    {
        public static Image ToImagePresentationModel(this ImageEntity imageEntity) =>
            Ioc.Resolve<IMapper>().Map<Image>(imageEntity);
    }
}
