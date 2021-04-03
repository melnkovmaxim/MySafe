using MySafe.Core.Entities.Responses;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Users.SignOutCommand
{
    public class SignOutCommandHandler : RequestHandlerBase<SignOutCommand, User>
    {
        public SignOutCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}