using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Documents.ChangeDocumentCommand
{
    /// <summary>
    ///     Изменить документ
    /// </summary>
    public class ChangeDocumentCommand: BearerRequestBase<DocumentEntity>
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int DocumentId { get; set; }
        public int FolderId { get; set; }

        public ChangeDocumentCommand(string name, string location, int documentId, int folderId)
        {
            Name = name;
            Location = location;
            DocumentId = documentId;
            FolderId = folderId;
        }

        public override Method RequestMethod => Method.PUT;
        public override string RequestResource => $"api/v1/documents/{DocumentId}";
    }
}