using Fody;
using MediatR;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Business.Extensions;
using MySafe.Core.Entities.Requests;
using MySafe.Core.Entities.Responses;
using MySafe.Data.Abstractions;
using User = MySafe.Core.Entities.Responses.User;

namespace MySafe.Business.Mediator.Users.TwoFactorAuthenticationCommand
{
    [ConfigureAwait(false)]
    public class TwoFactorAuthenticationCommandHandler : IRequestHandler<TwoFactorAuthenticationCommand, User>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;
        private readonly ISecureStorageRepository _secureStorageRepository;

        public TwoFactorAuthenticationCommandHandler(IRestClient restClient, IMapper mapper, ISecureStorageRepository secureStorageRepository)
        {
            _restClient = restClient;
            _mapper = mapper;
            _secureStorageRepository = secureStorageRepository;
        }

        public async Task<User> Handle(TwoFactorAuthenticationCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);

            var twoFactor = _mapper.Map<TwoFactorAuthenticationCommand, TwoFactor>(request);
            var serializedUser = JsonConvert.SerializeObject(twoFactor);
            var httpRequest = new RestRequest("users/two_factor_authentication", Method.PUT)
                .AddJsonBody(serializedUser);
            
            var cmdResponse = await _restClient.GetResponseAsync<User>(httpRequest, cancellationToken);
            
            await _secureStorageRepository.SetTokenAsync(cmdResponse.JwtToken.RawData);

            return cmdResponse;
        }
    }
}
