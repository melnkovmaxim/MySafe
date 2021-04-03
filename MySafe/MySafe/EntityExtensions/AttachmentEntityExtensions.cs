using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Core.Entities;
using MySafe.Core.Models;
using MySafe.Presentation.Models;

namespace MySafe.Presentation.EntityExtensions
{
    public static class AttachmentEntityExtensions
    {
        public static Attachment ToAttachmentPresentationModel(this AttachmentEntity attachmentEntity) => 
            Ioc.Resolve<IMapper>().Map<Attachment>(attachmentEntity);
    }
}
