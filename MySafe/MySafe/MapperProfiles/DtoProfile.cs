using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Core.Dto;
using Xamarin.Essentials;

namespace MySafe.Presentation.MapperProfiles
{
    public class DtoProfile: Profile
    {
        public DtoProfile()
        {
            CreateMap<FileResult, FileResultDto>()
                .ForMember(d => d.FileName, mo => mo.MapFrom(s => s.FileName))
                .ForMember(d => d.ContentType, mo => mo.MapFrom(s => s.ContentType))
                .ForMember(d => d.FileStream, mo => mo.MapFrom(s => s.OpenReadAsync()))
                ;
        }
    }
}
