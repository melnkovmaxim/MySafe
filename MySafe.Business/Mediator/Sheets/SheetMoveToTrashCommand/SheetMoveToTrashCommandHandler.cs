using MediatR;
using MySafe.Core.Entities.Responses;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Business.Extensions;
using MySafe.Business.Mediator.Abstractions;

namespace MySafe.Business.Mediator.Sheets.SheetMoveToTrashCommand
{
    public class MoveFileToTrashCommandHandler: RequestHandlerBase<SheetMoveToTrashCommand, Sheet>
    {
        public MoveFileToTrashCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}
