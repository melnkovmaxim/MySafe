﻿using AutoMapper;
using MySafe.Business.Mediator.Users.SignIn;
using MySafe.Business.Mediator.Users.SignInTwoFactor;
using MySafe.Core.Entities.Requests;
using MySafe.Core.Entities.Responses;
using MySafe.Presentation.Models;

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

            CreateMap<AttachmentResponse, Attachment>()
                .ForMember(d => d.Id, mo => mo.MapFrom(s => s.Id))
                .ForMember(d => d.Name, mo => mo.MapFrom(s => s.Name))
                .ForMember(d => d.Preview, mo => mo.MapFrom(s => s.Preview))
                .ForMember(d => d.FileExtension, mo => mo.MapFrom(s => s.FileExtension))
                ;

            CreateMap<DocumentResponse, Document>()
                .ForMember(d => d.Id, mo => mo.MapFrom(s => s.Id))
                .ForMember(d => d.Name, mo => mo.MapFrom(s => s.Name))
                .ForMember(d => d.Attachments, mo => mo.MapFrom(s => s.Attachments))
                .ForMember(d => d.CreatedAt, mo => mo.MapFrom(s => s.CreatedAt))
                .ForMember(d => d.FolderId, mo => mo.MapFrom(s => s.FolderId))
                ;

            CreateMap<FolderResponse, Folder>()
                .ForMember(d => d.Id, mo => mo.MapFrom(s => s.Id))
                .ForMember(d => d.Name, mo => mo.MapFrom(s => s.Name))
                .ForMember(d => d.Documents, mo => mo.MapFrom(s => s.Documents))
                ;

            CreateMap<TrashResponse, Trash>()
                .ForMember(d => d.FolderId, mo => mo.MapFrom(s => s.FolderId))
                .ForMember(d => d.ConstainsAttachments, mo => mo.MapFrom(s => s.ConstainsAttachments))
                .ForMember(d => d.Content, mo => mo.MapFrom(s => s.Content))
                .ForMember(d => d.Location, mo => mo.MapFrom(s => s.Location))
                .ForMember(d => d.TrashedAt, mo => mo.MapFrom(s => s.TrashedAt));
                ;
        }
    }
}
