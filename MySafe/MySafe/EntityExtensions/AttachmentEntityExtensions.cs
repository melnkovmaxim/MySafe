using AutoMapper;
using MySafe.Core.Entities;
using MySafe.Presentation.Models;

namespace MySafe.Presentation.EntityExtensions
{
    public static class AttachmentEntityExtensions
    {
        public static Attachment ToAttachmentPresentationModel(this AttachmentEntity attachmentEntity)
        {
            return Ioc.Resolve<IMapper>().Map<Attachment>(attachmentEntity);
        }
    }
}