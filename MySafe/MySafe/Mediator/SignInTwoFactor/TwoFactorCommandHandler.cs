using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Extensions;
using MySafe.Mediator.SignIn;
using MySafe.Models.Requests;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace MySafe.Mediator.SignInTwoFactor
{
    public class TwoFactorCommandHandler : IRequestHandler<TwoFactorCommand, JwtSecurityToken>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;

        public TwoFactorCommandHandler(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
        }

        public async Task<JwtSecurityToken> Handle(TwoFactorCommand request, CancellationToken cancellationToken)
        {
            var twoFactor = _mapper.Map<TwoFactorCommand, TwoFactor>(request);
            var serializedUser = JsonConvert.SerializeObject(twoFactor);
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);

            var httpRequest = new RestRequest("users/two_factor_authentication", Method.PUT)
                .AddJsonBody(serializedUser);
            var response = await _restClient.ExecuteAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            var jwtToken = new JwtSecurityTokenHandler()
                .GetJwtTokenFromResponse(response);

            return jwtToken;
        }
    }
}
