using MediatR;
using MySafe.Core.Entities.Responses;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Business.Extensions;
using MySafe.Business.Mediator.Abstractions;

namespace MySafe.Business.Mediator.Images.ImageMoveToTrashCommand
{
    public class ImageMoveToTrashCommandHandler: RequestHandlerBase<DestroyTrashImageCommand.DestroyTrashImageCommand, Image>
    {
        public ImageMoveToTrashCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}
