using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MySafe.Extensions;
using MySafe.Models.MediatorResponses;
using MySafe.Repositories.Abstractions;
using MySafe.Services.Abstractions;
using RestSharp;
using RestSharp.Authenticators;

namespace MySafe.Mediator.SignOut
{
    public class SignOutCommandHandler : IRequestHandler<SignOutCommand, UserResponse>
    {
        private readonly IRestClient _restClient;

        public SignOutCommandHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }
        
        public async Task<UserResponse> Handle(SignOutCommand request, CancellationToken cancellationToken)
        {
            var cmdResponse = new UserResponse();

            try
            {
                _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
                var httpRequest = new RestRequest("users/sign_out", Method.DELETE);
                var response = await _restClient.ExecuteAsync(httpRequest, cancellationToken);

                if (!response.IsSuccessful)
                {
                    throw response.ErrorException;
                }

                await Ioc.Resolve<ISecureStorageRepository>().RemoveToken();
            }
            catch (Exception ex)
            {
                cmdResponse.Error = ex.Message;
            }

            return cmdResponse;
        }
    }
}
