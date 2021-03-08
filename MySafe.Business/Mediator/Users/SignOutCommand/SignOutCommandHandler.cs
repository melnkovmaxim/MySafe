using Fody;
using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Business.Extensions;
using MySafe.Core.Entities.Responses;
using MySafe.Data.Abstractions;

namespace MySafe.Business.Mediator.Users.SignOutCommand
{
    [ConfigureAwait(false)]
    public class SignOutCommandHandler : IRequestHandler<SignOutCommand, User>
    {
        private readonly IRestClient _restClient;
        private readonly ISecureStorageRepository _secureStorageRepository;

        public SignOutCommandHandler(IRestClient restClient, ISecureStorageRepository secureStorageRepository)
        {
            _restClient = restClient;
            _secureStorageRepository = secureStorageRepository;
        }
        
        public async Task<User> Handle(SignOutCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);

            var httpRequest = new RestRequest("users/sign_out", Method.DELETE);
            var cmdResponse = await _restClient.GetResponseAsync<User>(httpRequest, cancellationToken);
            
            await _secureStorageRepository.RemoveToken();

            return cmdResponse;
        }
    }
}
