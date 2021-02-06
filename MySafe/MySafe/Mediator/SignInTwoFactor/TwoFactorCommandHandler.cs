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
using MySafe.Models.MediatorResponses;
using MySafe.Models.Requests;
using MySafe.Repositories.Abstractions;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace MySafe.Mediator.SignInTwoFactor
{
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
            var commandResponse = new UserResponse();

            try
            {
                var twoFactor = _mapper.Map<TwoFactorCommand, TwoFactor>(request);
                var serializedUser = JsonConvert.SerializeObject(twoFactor);

                _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);

                var httpRequest = new RestRequest("users/two_factor_authentication", Method.PUT)
                    .AddJsonBody(serializedUser);

                var response = await _restClient.ExecuteAsync(httpRequest, cancellationToken)
                    .ConfigureAwait(false);
                
                if (!response.IsSuccessful)
                {
                    throw response.ErrorException;
                }
                
                var jwtToken = new JwtSecurityTokenHandler()
                    .GetJwtTokenFromResponse(response);

                await Ioc.Resolve<ISecureStorageRepository>()
                    .SetTokenAsync(jwtToken.RawData);
            }
            catch (Exception ex)
            {
                commandResponse.Error = ex.Message;
            }

            return commandResponse;
        }
    }
}
