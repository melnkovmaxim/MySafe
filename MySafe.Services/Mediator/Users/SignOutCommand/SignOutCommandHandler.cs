using AutoMapper;
using MySafe.Core.Models.JsonRequests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Users.SignOutCommand
{
    public class SignOutCommandHandler : RequestHandlerBase<SignOutCommand, UserJsonBody, UserEntity>
    {
        public SignOutCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}