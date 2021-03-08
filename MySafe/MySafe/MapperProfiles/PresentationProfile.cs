using AutoMapper;
using MySafe.Business.Mediator.Users.SignInCommand;
using MySafe.Business.Mediator.Users.TwoFactorAuthenticationCommand;
using MySafe.Core.Entities.Requests;
using MySafe.Core.Entities.Responses;
using MySafe.Presentation.Models;
using Document = MySafe.Core.Entities.Responses.Document;
using Folder = MySafe.Core.Entities.Responses.Folder;
using User = MySafe.Core.Entities.Requests.User;

namespace MySafe.Presentation.MapperProfiles
{
    public class PresentationProfile : Profile
    {
        public PresentationProfile()
        {
            CreateMap<SignInCommand, User>()
                .ForMember(d => d.Login, opt => opt.MapFrom(x => x.User.Login))
                .ForMember(d => d.Password, opt => opt.MapFrom(x => x.User.Password));

            CreateMap<TwoFactorAuthenticationCommand, TwoFactor>()
                .ForMember(d => d.Code, opt => opt.MapFrom(x => x.Code));

            CreateMap<AttachmentResponse, Attachment>()
                .ForMember(d => d.Id, mo => mo.MapFrom(s => s.Id))
                .ForMember(d => d.Name, mo => mo.MapFrom(s => s.Name))
                .ForMember(d => d.Preview, mo => mo.MapFrom(s => s.Preview))
                .ForMember(d => d.FileExtension, mo => mo.MapFrom(s => s.FileExtension))
                ;

            CreateMap<Document, Models.Document>()
                .ForMember(d => d.Id, mo => mo.MapFrom(s => s.Id))
                .ForMember(d => d.Name, mo => mo.MapFrom(s => s.Name))
                .ForMember(d => d.Attachments, mo => mo.MapFrom(s => s.Attachments))
                .ForMember(d => d.CreatedAt, mo => mo.MapFrom(s => s.CreatedAt))
                .ForMember(d => d.FolderId, mo => mo.MapFrom(s => s.FolderId))
                ;

            CreateMap<Folder, Models.Folder>()
                .ForMember(d => d.Id, mo => mo.MapFrom(s => s.Id))
                .ForMember(d => d.Name, mo => mo.MapFrom(s => s.Name))
                .ForMember(d => d.Documents, mo => mo.MapFrom(s => s.Documents))
                ;

            CreateMap<MySafe.Core.Entities.Responses.Trash, MySafe.Presentation.Models.Trash>()
                .ForMember(d => d.FolderId, mo => mo.MapFrom(s => s.FolderId))
                .ForMember(d => d.ConstainsAttachments, mo => mo.MapFrom(s => s.ConstainsAttachments))
                .ForMember(d => d.Content, mo => mo.MapFrom(s => s.Content))
                .ForMember(d => d.Location, mo => mo.MapFrom(s => s.Location))
                .ForMember(d => d.TrashedAt, mo => mo.MapFrom(s => s.TrashedAt));
                ;
        }
    }
}
