using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Business.Extensions;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Images.RestoreTrashImageCommand
{
    public class RestoreTrashImageCommandHandler: RequestHandlerBase<RestoreTrashImageCommand, Image>
    {
        public RestoreTrashImageCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}
