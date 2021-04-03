using AutoMapper;
using MySafe.Core.Entities;
using MySafe.Core.Models.Responses;
using MySafe.Presentation.Models;

namespace MySafe.Presentation.MapperProfiles
{
    public class PresentationProfile : Profile
    {
        public PresentationProfile()
        {
            CreateMap<SafeEntity, Safe>()
                .ForMember(d => d.Capacity, mo => mo.MapFrom(s => s.Capacity))
                .ForMember(d => d.UsedCapacity, mo => mo.MapFrom(s => s.UsedCapacity))
                .ForMember(d => d.Folders, mo => mo.MapFrom(s => s.Folders))
                .ReverseMap()
                ;

            CreateMap<DocumentEntity, Document>()
                .ForMember(d => d.Id, mo => mo.MapFrom(s => s.Id))
                .ForMember(d => d.Name, mo => mo.MapFrom(s => s.Name))
                .ForMember(d => d.Attachments, mo => mo.MapFrom(s => s.Attachments))
                .ForMember(d => d.CreatedAt, mo => mo.MapFrom(s => s.CreatedAt))
                .ForMember(d => d.FolderId, mo => mo.MapFrom(s => s.FolderId))
                .ReverseMap()
                ;

            CreateMap<FolderEntity, Folder>()
                .ForMember(d => d.Id, mo => mo.MapFrom(s => s.Id))
                .ForMember(d => d.Name, mo => mo.MapFrom(s => s.Name))
                .ForMember(d => d.Documents, mo => mo.MapFrom(s => s.Documents))
                .ReverseMap()
                ;

            CreateMap<SheetEntity, Sheet>()
                .ForMember(d => d.FileBytes, mo => mo.MapFrom(s => s.FileBytes))
                .ForMember(d => d.Error, mo => mo.MapFrom(s => s.Error))
                .ReverseMap();

            CreateMap<ImageEntity, Image>()
                .ForMember(d => d.FileBytes, mo => mo.MapFrom(s => s.FileBytes))
                .ForMember(d => d.Error, mo => mo.MapFrom(s => s.Error))
                .ReverseMap();

            CreateMap<NoteEntity, Note>()
                .ForMember(d => d.Id, mo => mo.MapFrom(s => s.Id))
                .ForMember(d => d.ClippedContent, mo => mo.MapFrom(s => s.ClippedContent))
                .ReverseMap();

            CreateMap<TrashEntity, Trash>()
                .ForMember(d => d.FolderId, mo => mo.MapFrom(s => s.FolderId))
                .ForMember(d => d.ConstainsAttachments, mo => mo.MapFrom(s => s.ConstainsAttachments))
                .ForMember(d => d.Content, mo => mo.MapFrom(s => s.Content))
                .ForMember(d => d.Location, mo => mo.MapFrom(s => s.Location))
                .ForMember(d => d.TrashedAt, mo => mo.MapFrom(s => s.TrashedAt))
                .ReverseMap()
                ;
            ;

            CreateMap<AttachmentEntity, Attachment>()
                .ForMember(d => d.Id, mo => mo.MapFrom(s => s.Id))
                .ForMember(d => d.Name, mo => mo.MapFrom(s => s.Name))
                .ForMember(d => d.Preview, mo => mo.MapFrom(s => s.Preview))
                .ForMember(d => d.FileExtension, mo => mo.MapFrom(s => s.FileExtension))
                .ReverseMap()
                ;
        }
    }
}