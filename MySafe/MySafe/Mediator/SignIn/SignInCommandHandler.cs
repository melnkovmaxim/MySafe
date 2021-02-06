using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MySafe.Extensions;
using MySafe.Models.MediatorResponses;
using MySafe.Models.Requests;
using RestSharp;

namespace MySafe.Mediator.SignIn
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, UserResponse>
    {
        private readonly IRestClient _restClient;
        private readonly IMapper _mapper;

        public SignInCommandHandler(IRestClient restClient, IMapper mapper)
        {
            this._restClient = restClient;
            _mapper = mapper;
        }

        public async Task<UserResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var commandResponse = new UserResponse();

            try
            {
                var serializedUser = _mapper.Map<SignInCommand, User>(request)
                    .SerializeWithRoot();

                var httpRequest = new RestRequest("users/sign_in", Method.POST)
                    .AddJsonBody(serializedUser);

                var response = await _restClient.ExecuteAsync(httpRequest, cancellationToken)
                    .ConfigureAwait(false);

                if (!response.IsSuccessful)
                {
                    throw response.ErrorException;
                }

                var jwtToken = new JwtSecurityTokenHandler()
                    .GetJwtTokenFromResponse(response);

                commandResponse.JwtToken = jwtToken;
            }
            catch (Exception ex)
            {
                commandResponse.Error = ex.Message;
            }

            return commandResponse;
        }
    }
}
