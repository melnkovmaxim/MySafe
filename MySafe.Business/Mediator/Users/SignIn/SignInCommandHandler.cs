using Fody;
using MediatR;
using RestSharp;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Business.Extensions;
using MySafe.Core.Entities.Requests;
using MySafe.Core.Entities.Responses;

namespace MySafe.Business.Mediator.Users.SignIn
{
    [ConfigureAwait(false)]
    public class SignInCommandHandler : IRequestHandler<SignInCommand, UserResponse>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;

        public SignInCommandHandler(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
        }

        public async Task<UserResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var serializedUser = _mapper.Map<SignInCommand, User>(request)
                .SerializeWithRoot();

            var httpRequest = new RestRequest("users/sign_in", Method.POST)
                .AddJsonBody(serializedUser);

            var cmdResponse = await _restClient.GetResponseAsync<UserResponse>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
