using AutoMapper;
using Fody;
using MediatR;
using MySafe.Presentation.Models.Requests;
using MySafe.Presentation.Models.Responses;
using MySafe.Presentation.Repositories.Abstractions;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using MySafe.Presentation.Extensions;

namespace MySafe.Presentation.Mediator.Users.SignInTwoFactor
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
