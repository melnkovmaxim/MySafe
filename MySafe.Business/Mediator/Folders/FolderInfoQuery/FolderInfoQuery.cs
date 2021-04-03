using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Folders.FolderInfoQuery
{
    /// <summary>
    ///     Получить содержимое ячейки
    /// </summary>
    public class FolderInfoQuery : BearerRequestBase<Folder>
    {
        public FolderInfoQuery(int documentId)
        {
            DocumentId = documentId;
        }

        public int DocumentId { get; set; }

        public override Method RequestMethod => Method.GET;
        public override string RequestResource => $"api/v1/folders/{DocumentId}";
    }
}