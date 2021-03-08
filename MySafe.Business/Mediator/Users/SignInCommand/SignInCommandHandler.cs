using Fody;
using MediatR;
using RestSharp;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Business.Extensions;
using MySafe.Core.Entities.Requests;
using MySafe.Core.Entities.Responses;
using User = MySafe.Core.Entities.Responses.User;

namespace MySafe.Business.Mediator.Users.SignInCommand
{
    [ConfigureAwait(false)]
    public class SignInCommandHandler : IRequestHandler<SignInCommand, User>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;

        public SignInCommandHandler(IRestClient restClient, IMapper mapper)
        {
            _restClient = restClient;
            _mapper = mapper;
        }

        public async Task<User> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var serializedUser = _mapper.Map<SignInCommand, Core.Entities.Requests.User>(request)
                .SerializeWithRoot();

            var httpRequest = new RestRequest("users/sign_in", Method.POST)
                .AddJsonBody(serializedUser);

            var cmdResponse = await _restClient.GetResponseAsync<User>(httpRequest, cancellationToken);

            return cmdResponse;
        }
    }
}
