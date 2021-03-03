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

namespace MySafe.Business.Mediator.Users.SignInTwoFactor
{
    [ConfigureAwait(false)]
    public class TwoFactorCommandHandler : IRequestHandler<TwoFactorCommand, UserResponse>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;
        private readonly ISecureStorageRepository _secureStorageRepository;

        public TwoFactorCommandHandler(IRestClient restClient, IMapper mapper, ISecureStorageRepository secureStorageRepository)
        {
            _restClient = restClient;
            _mapper = mapper;
            _secureStorageRepository = secureStorageRepository;
        }

        public async Task<UserResponse> Handle(TwoFactorCommand request, CancellationToken cancellationToken)
        {
            _restClient.Authenticator = new JwtAuthenticator(request.JwtToken.RawData);

            var twoFactor = _mapper.Map<TwoFactorCommand, TwoFactor>(request);
            var serializedUser = JsonConvert.SerializeObject(twoFactor);
            var httpRequest = new RestRequest("users/two_factor_authentication", Method.PUT)
                .AddJsonBody(serializedUser);
            
            var cmdResponse = await _restClient.GetResponseAsync<UserResponse>(httpRequest, cancellationToken);
            
            await _secureStorageRepository.SetTokenAsync(cmdResponse.JwtToken.RawData);

            return cmdResponse;
        }
    }
}
