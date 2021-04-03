using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Core.Entities.Abstractions;
using MySafe.Presentation.Models.Abstractions;

namespace MySafe.Presentation.EntityExtensions
{
    public static class PresentationModelExtensions
    {
        public static IPresentationModel ToPresentationModel(this IEntity entity) =>
            Ioc.Resolve<IMapper>().Map<IPresentationModel>(entity);
    }
}
