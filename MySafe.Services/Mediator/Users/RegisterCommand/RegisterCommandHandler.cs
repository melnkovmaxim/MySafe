using AutoMapper;
using MySafe.Core.Models.Requests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Users.RegisterCommand
{
    public class RegisterCommandHandler : RequestHandlerBase<RegisterCommand, UserJsonBody, UserEntity>
    {
        public RegisterCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}