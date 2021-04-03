using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Core.Models.Responses;
using MySafe.Presentation.Models;

namespace MySafe.Presentation.EntityExtensions
{
    public static class FolderEntityExtensions
    {
        public static Folder ToFolderPresentationModel(this FolderEntity folderEntity) =>
            Ioc.Resolve<IMapper>().Map<Folder>(folderEntity);
    }
}
