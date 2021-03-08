using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Business.Extensions;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses.Abstractions;

namespace MySafe.Business.Mediator.Trash.ClearTrashCommand
{
    public class ClearTrashCommandHandler: RequestHandlerBase<ClearTrashCommand, ResponseBase>
    {
        public ClearTrashCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}
