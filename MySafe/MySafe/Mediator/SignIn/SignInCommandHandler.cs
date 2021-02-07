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
using Xamarin.Essentials;

namespace MySafe.Mediator.SignIn
{
    [ConfigureAwait(false)]
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
            var cmdResponse = new UserResponse();

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
                    var errorResponse = JsonConvert.DeserializeObject<BaseResponse>(response.Content);
                    throw new HttpRequestException(errorResponse.Error);
                }

                var jwtToken = new JwtSecurityTokenHandler()
                    .GetJwtTokenFromResponse(response);

                cmdResponse.JwtToken = jwtToken;
            }
            catch (Exception ex)
            {
                cmdResponse.Error = ex.Message;
            }

            return cmdResponse;
        }
    }
}
