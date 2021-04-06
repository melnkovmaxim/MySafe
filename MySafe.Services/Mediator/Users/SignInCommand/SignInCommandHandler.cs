using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Users.SignInCommand
{
    public class SignInCommandHandler : RequestHandlerBase<SignInCommand, SerializedUserJsonBody, UserEntity>
    {
        public SignInCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}