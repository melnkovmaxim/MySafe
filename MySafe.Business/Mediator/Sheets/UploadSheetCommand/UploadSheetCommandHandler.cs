using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Sheets.UploadSheetCommand
{
    public class UploadFileCommandHandler: RequestHandlerBase<UploadSheetCommand, Sheet>
    {
        public UploadFileCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}
