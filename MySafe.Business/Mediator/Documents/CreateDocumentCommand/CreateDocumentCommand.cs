using MySafe.Core.Entities.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Documents.CreateDocumentCommand
{
    /// <summary>
    ///     Создать новый документ
    /// </summary>
    public class CreateDocumentCommand : BearerRequestBase<Document>
    {
        public CreateDocumentCommand(int folderId)
        {
            FolderId = folderId;
        }

        public int FolderId { get; set; }
        public override Method RequestMethod => Method.POST;
        public override string RequestResource => $"/api/v1/folders/{FolderId}/documents";
    }
}