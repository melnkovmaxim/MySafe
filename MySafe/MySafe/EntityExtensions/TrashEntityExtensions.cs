using AutoMapper;
using MySafe.Core.Models.Responses;
using MySafe.Presentation.Models;

namespace MySafe.Presentation.EntityExtensions
{
    public static class TrashEntityExtensions
    {
        public static Trash ToTrashPresentationModel(this TrashEntity trashEntity)
        {
            return Ioc.Resolve<IMapper>().Map<Trash>(trashEntity);
        }
    }
}