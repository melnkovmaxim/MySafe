using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Services.Mediator.Documents.ChangeDocumentCommand;
using MySafe.Services.Mediator.Documents.CreateDocumentCommand;
using MySafe.Services.Mediator.Documents.DestroyTrashDocumentCommand;
using MySafe.Services.Mediator.Documents.DocumentInfoQuery;
using MySafe.Services.Mediator.Documents.DocumentMoveToTrashCommand;
using MySafe.Services.Mediator.Documents.RestoreTrashDocumentCommand;
using MySafe.Services.Mediator.Folders.ChangeFolderCommand;
using MySafe.Services.Mediator.Folders.FolderInfoQuery;
using MySafe.Services.Mediator.Folders.FolderListQuery;
using MySafe.Services.Mediator.Images.ChangeImageCommand;
using MySafe.Services.Mediator.Images.DestroyTrashImageCommand;
using MySafe.Services.Mediator.Images.ImageMoveToTrashCommand;
using MySafe.Services.Mediator.Images.OriginalImageQuery;
using MySafe.Services.Mediator.Images.RestoreTrashImageCommand;
using MySafe.Services.Mediator.Images.SmallSizeImageQuery;
using MySafe.Services.Mediator.Images.UploadImageCommand;
using MySafe.Services.Mediator.Notes.ChangeNoteCommand;
using MySafe.Services.Mediator.Notes.CreateNoteCommand;
using MySafe.Services.Mediator.Notes.NoteInfoQuery;
using MySafe.Services.Mediator.Notes.NoteListQuery;
using MySafe.Services.Mediator.Notes.RemoveNoteCommand;
using MySafe.Services.Mediator.Safe.SafeInfoQuery;
using MySafe.Services.Mediator.Trash.ClearTrashCommand;
using MySafe.Services.Mediator.Users.ConfirmationInstructionsQuery;
using MySafe.Services.Mediator.Users.RefreshTokenQuery;
using MySafe.Services.Mediator.Users.RegisterCommand;
using MySafe.Services.Mediator.Users.SignInCommand;
using MySafe.Services.Mediator.Users.SignOutCommand;
using MySafe.Services.Mediator.Users.TwoFactorAuthenticationCommand;

namespace MySafe.Services.MapperProfiles
{
    public class MediatorQueryCommandProfile : Profile
    {
        public MediatorQueryCommandProfile()
        {
            // Users
            CreateMap<ConfirmationInstructionsQuery, UserJsonBody>();
            CreateMap<RegisterCommand, UserJsonBody>()
                .ForMember(d => d.Login, mo => mo.MapFrom(s => s.Login))
                .ForMember(d => d.Password, mo => mo.MapFrom(s => s.Password))
                .ForMember(d => d.PasswordConfirmation, mo => mo.MapFrom(s => s.PasswordConfirmation))
                .ForMember(d => d.Email, mo => mo.MapFrom(s => s.Email))
                .ForMember(d => d.PhoneNumber, mo => mo.MapFrom(s => s.PhoneNumber))
                .ForMember(d => d.UserAgreement, mo => mo.MapFrom(s => s.UserAgreement));

            CreateMap<SignInCommand, SerializedUserJsonBody>()
                .ForMember(d => d.Login, mo => mo.MapFrom(s => s.Login))
                .ForMember(d => d.Password, mo => mo.MapFrom(s => s.Password));

            CreateMap<SignInCommand, UserJsonBody>()
                .ForMember(d => d.Login, mo => mo.MapFrom(s => s.Login))
                .ForMember(d => d.Password, mo => mo.MapFrom(s => s.Password));

            CreateMap<SignOutCommand, UserJsonBody>();
            CreateMap<TwoFactorAuthenticationCommand, UserJsonBody>()
                .ForMember(d => d.EmailCode, mo => mo.MapFrom(s => s.Code));

            CreateMap<RefreshTokenQuery, UserJsonBody>()
                .ForMember(d => d.RefreshToken, mo => mo.MapFrom(s => s.RefreshToken));

            //Safe
            CreateMap<ClearTrashCommand, SafeJsonBody>();
            CreateMap<SafeInfoQuery, SafeJsonBody>();

            //Notes
            CreateMap<ChangeNoteCommand, NoteJsonBody>()
                .ForMember(d => d.Content, mo => mo.MapFrom(s => s.Content));

            CreateMap<CreateNoteCommand, NoteJsonBody>()
                .ForMember(d => d.Content, mo => mo.MapFrom(s => s.Content));

            CreateMap<NoteInfoQuery, NoteJsonBody>();
            CreateMap<NoteListQuery, NoteJsonBody>();
            CreateMap<RemoveNoteCommand, NoteJsonBody>();

            //Images
            CreateMap<ChangeImageCommand, ImagesJsonBody>()
                .ForMember(d => d.Rotate, mo => mo.MapFrom(s => s.Rotate));

            CreateMap<DestroyTrashImageCommand, ImagesJsonBody>();
            CreateMap<ImageMoveToTrashCommand, ImagesJsonBody>();
            CreateMap<OriginalImageQuery, ImagesJsonBody>();
            CreateMap<RestoreTrashImageCommand, ImagesJsonBody>();
            CreateMap<SmallSizeImageQuery, ImagesJsonBody>();
            CreateMap<UploadImageCommand, ImagesJsonBody>();

            //Folders
            CreateMap<ChangeFolderCommand, FolderJsonBody>();
            CreateMap<FolderInfoQuery, FolderJsonBody>();
            CreateMap<FolderListQuery, FolderJsonBody>();

            //Documents
            CreateMap<ChangeDocumentCommand, DocumentJsonBody>();
            CreateMap<CreateDocumentCommand, DocumentJsonBody>();
            CreateMap<DestroyTrashDocumentCommand, DocumentJsonBody>();
            CreateMap<DocumentInfoQuery, DocumentJsonBody>();
            CreateMap<DocumentMoveToTrashCommand, DocumentJsonBody>();
            CreateMap<RestoreTrashDocumentCommand, DocumentJsonBody>()
                .ForMember(d => d.FolderId, mo => mo.MapFrom(s => s.FolderId));
        }
    }
}