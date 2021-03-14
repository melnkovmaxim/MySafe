using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Business.Extensions;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Sheets.RestoreTrashSheetCommand
{
    public class RestoreTrashSheetCommandHandler: RequestHandlerBase<RestoreTrashSheetCommand, Sheet>
    {
        public RestoreTrashSheetCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}
