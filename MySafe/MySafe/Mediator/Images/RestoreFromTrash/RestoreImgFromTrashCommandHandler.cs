using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Extensions;
using MySafe.Mediator.Images.RemoveFromTrash;
using MySafe.Models.Responses;
using RestSharp;
using RestSharp.Authenticators;

namespace MySafe.Mediator.Images.RestoreFromTrash
{
    public class RestoreImgFromTrashCommandHandler: IRequestHandler<RestoreImgFromTrashCommand, ImageResponse>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;
        
        public RestoreImgFromTrashCommandHandler(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
        }

        public async Task<ImageResponse> Handle(RestoreImgFromTrashCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest($"/api/v1/images/{request.ImageId}/restore", Method.PUT);
            var cmdResponse = await _restClient.GetResponseAsync<ImageResponse>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
