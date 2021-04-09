using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Presentation.Models;
using MySafe.Services.Mediator.Users.RegisterCommand;

namespace MySafe.Presentation.MapperProfiles
{
    public class CommandQueryProfile: Profile
    {
        public CommandQueryProfile()
        {
            CreateMap<User, RegisterCommand>()
                .ForMember(d => d.Login, mo => mo.MapFrom(s => s.Login))
                .ForMember(d => d.Password, mo => mo.MapFrom(s => s.Password))
                .ForMember(d => d.PasswordConfirmation, mo => mo.MapFrom(s => s.PasswordConfirmation))
                .ForMember(d => d.Email, mo => mo.MapFrom(s => s.Email))
                .ForMember(d => d.PhoneNumber, mo => mo.MapFrom(s => s.PhoneNumber))
                .ForMember(d => d.UserAgreement, mo => mo.MapFrom(s => s.UserAgreement))
                .ForAllOtherMembers(options => options.Ignore())
                ;
        }
    }
}
