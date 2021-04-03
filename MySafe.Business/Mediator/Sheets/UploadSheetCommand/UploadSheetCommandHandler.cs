using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Sheets.UploadSheetCommand
{
    public class UploadFileCommandHandler : RequestHandlerBase<UploadSheetCommand, EmptyJsonBody, SheetEntity>
    {
        public UploadFileCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}