using AutoMapper;
using MySafe.Mediator.SignIn;
using MySafe.Models.Requests;

namespace MySafe.MapperProfiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<SignInCommand, User>()
                .ForMember(d => d.Login, opt => opt.MapFrom(x => x.Login))
                .ForMember(d => d.Password, opt => opt.MapFrom(x => x.Password));
        }
    }
}
