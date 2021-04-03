using AutoMapper;
using MySafe.Core.Models.Responses;
using MySafe.Presentation.Models;

namespace MySafe.Presentation.EntityExtensions
{
    public static class UserEntityExtensions
    {
        public static User ToUserPresentationModel(this UserEntity userEntity)
        {
            return Ioc.Resolve<IMapper>().Map<User>(userEntity);
        }
    }
}