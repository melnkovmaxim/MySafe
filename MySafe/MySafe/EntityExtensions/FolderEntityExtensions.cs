using AutoMapper;
using MySafe.Core.Models.Responses;
using MySafe.Presentation.Models;

namespace MySafe.Presentation.EntityExtensions
{
    public static class FolderEntityExtensions
    {
        public static Folder ToFolderPresentationModel(this FolderEntity folderEntity)
        {
            return Ioc.Resolve<IMapper>().Map<Folder>(folderEntity);
        }
    }
}