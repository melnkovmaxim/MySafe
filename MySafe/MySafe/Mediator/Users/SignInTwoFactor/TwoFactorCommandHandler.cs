using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fody;
using MediatR;
using MySafe.Extensions;
using MySafe.Models.Requests;
using MySafe.Models.Responses;
using MySafe.Repositories.Abstractions;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace MySafe.Mediator.Users.SignInTwoFactor
{
    [ConfigureAwait(false)]
    public class TwoFactorCommandHandler : IRequestHandler<TwoFactorCommand, UserResponse>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;

        public TwoFactorCommandHandler(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
        }

        public async Task<UserResponse> Handle(TwoFactorCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);

            var twoFactor = _mapper.Map<TwoFactorCommand, TwoFactor>(request);
            var serializedUser = JsonConvert.SerializeObject(twoFactor);
            var httpRequest = new RestRequest("users/two_factor_authentication", Method.PUT)
                .AddJsonBody(serializedUser);
            
            var cmdResponse = await _restClient.GetResponseAsync<UserResponse>(httpRequest, cancellationToken);
            
            await Ioc.Resolve<ISecureStorageRepository>()
                .SetTokenAsync(cmdResponse.JwtToken.RawData);

            return cmdResponse;
        }
    }
}
