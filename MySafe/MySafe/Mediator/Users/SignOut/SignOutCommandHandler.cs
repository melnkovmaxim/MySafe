using Fody;
using MediatR;
using MySafe.Presentation.Models.Responses;
using MySafe.Presentation.Repositories.Abstractions;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Presentation.Extensions;

namespace MySafe.Presentation.Mediator.Users.SignOut
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
