using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MySafe.Extensions;
using RestSharp;
using RestSharp.Authenticators;

namespace MySafe.Mediator.SignOut
{
    public class SignOutCommandHandler : IRequestHandler<SignOutCommand>
    {
        private readonly IRestClient _restClient;

        public SignOutCommandHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }
        
        public async Task<Unit> Handle(SignOutCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);
            var httpRequest = new RestRequest("users/sign_out", Method.DELETE);
            _ = await _restClient.ExecuteAsync(httpRequest, cancellationToken);

            return Unit.Value;
        }
    }
}
