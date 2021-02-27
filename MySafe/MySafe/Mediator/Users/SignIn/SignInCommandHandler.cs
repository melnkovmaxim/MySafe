using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DryIoc;
using Fody;
using MediatR;
using MySafe.Extensions;
using MySafe.Models.Requests;
using MySafe.Models.Responses;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using Xamarin.Essentials;

namespace MySafe.Mediator.Users.SignIn
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
