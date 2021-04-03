using MySafe.Core.Entities.Responses;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Users.RegisterCommand
{
    public class RegisterCommandHandler : RequestHandlerBase<RegisterCommand, User>
    {
        public RegisterCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}