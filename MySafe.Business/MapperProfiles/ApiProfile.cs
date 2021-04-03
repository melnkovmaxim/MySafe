using System.Collections.Generic;
using AutoMapper;
using MySafe.Core.Entities.Responses;
using MySafe.Core.Entities.Responses.Abstractions;
using MySafe.Core.Models.Responses;
using MySafe.Services.Extensions;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization;

namespace MySafe.Services.MapperProfiles
{
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<IRestResponse, List<IResponse>>()
                .ForAllMembers(s => s.Ignore());

            CreateMap<IRestResponse, User>()
                .ForAllMembers(s => s.Ignore());
            CreateMap<IRestResponse, Document>()
                .ForAllMembers(s => s.Ignore());
            CreateMap<IRestResponse, Folder>()
                .ForAllMembers(s => s.Ignore());
            CreateMap<IRestResponse, Image>()
                .ForAllMembers(s => s.Ignore());
            CreateMap<IRestResponse, Sheet>()
                .ForAllMembers(s => s.Ignore());
            CreateMap<IRestResponse, TrashResponse>()
                .ForAllMembers(s => s.Ignore());
            CreateMap<IRestResponse, Safe>()
                .ForAllMembers(s => s.Ignore());
            CreateMap<IRestResponse, Note>()
                .ForAllMembers(s => s.Ignore());

            CreateMap<IRestResponse, IResponse>()
                .Include<IRestResponse, User>()
                .Include<IRestResponse, Document>()
                .Include<IRestResponse, Folder>()
                .Include<IRestResponse, Image>()
                .Include<IRestResponse, Sheet>()
                .Include<IRestResponse, TrashResponse>()
                .Include<IRestResponse, Safe>()
                .Include<IRestResponse, Note>()
                .AfterMap((s, d) =>
                {
                    //if (!s.IsSuccessful)
                    //{
                    //    //d = (IResponse) Activator.CreateInstance(d.GetType());
                    //    d.Error = s?.ErrorMessage;
                    //    return;
                    //}

                    if (s?.ContentType == ContentType.Json)
                        d = JsonConvert.DeserializeObject(s.Content, d.GetType()) as IResponse;
                    else
                        d.FileBytes = s?.RawBytes;

                    if (s is User userResponse) userResponse.JwtToken = s.GetJwtToken();
                });
        }
    }
}