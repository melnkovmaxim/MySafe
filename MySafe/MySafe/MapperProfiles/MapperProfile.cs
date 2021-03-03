using AutoMapper;
using MySafe.Presentation.Mediator.Users.SignIn;
using MySafe.Presentation.Mediator.Users.SignInTwoFactor;
using MySafe.Presentation.Models.Requests;

namespace MySafe.Presentation.MapperProfiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<SignInCommand, User>()
                .ForMember(d => d.Login, opt => opt.MapFrom(x => x.Login))
                .ForMember(d => d.Password, opt => opt.MapFrom(x => x.Password));

            CreateMap<TwoFactorCommand, TwoFactor>()
                .ForMember(d => d.Code, opt => opt.MapFrom(x => x.Code));
        }
    }
}
