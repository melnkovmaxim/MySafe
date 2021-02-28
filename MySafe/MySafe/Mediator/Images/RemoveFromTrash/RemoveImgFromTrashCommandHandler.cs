using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Extensions;
using MySafe.Models.Responses;
using RestSharp;
using RestSharp.Authenticators;

namespace MySafe.Mediator.Images.RemoveFromTrash
{
    public class RemoveImgFromTrashCommandHandler: IRequestHandler<RemoveImgFromTrashCommand, ImageResponse>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;
        
        public RemoveImgFromTrashCommandHandler(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
        }

        public async Task<ImageResponse> Handle(RemoveImgFromTrashCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest($"/api/v1/images/{request.ImageId}", Method.DELETE);
            var cmdResponse = await _restClient.GetResponseAsync<ImageResponse>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
