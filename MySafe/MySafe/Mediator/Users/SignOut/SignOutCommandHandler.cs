using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fody;
using MediatR;
using MySafe.Extensions;
using MySafe.Models.Responses;
using MySafe.Repositories.Abstractions;
using MySafe.Services.Abstractions;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace MySafe.Mediator.Users.SignOut
{
    [ConfigureAwait(false)]
    public class SignOutCommandHandler : IRequestHandler<SignOutCommand, UserResponse>
    {
        private readonly IRestClient _restClient;

        public SignOutCommandHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }
        
        public async Task<UserResponse> Handle(SignOutCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);

            var httpRequest = new RestRequest("users/sign_out", Method.DELETE);
            var cmdResponse = await _restClient.GetResponseAsync<UserResponse>(httpRequest, cancellationToken);
            
            await Ioc.Resolve<ISecureStorageRepository>().RemoveToken();

            return cmdResponse;
        }
    }
}
