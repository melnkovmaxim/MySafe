using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Business.Extensions;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Sheets.DestroyTrashSheetCommand
{
    public class RemoveFileFromTrashCommandHandler: RequestHandlerBase<DestroyTrashSheetCommand, Sheet>
    {
        public RemoveFileFromTrashCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}
