using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Users.SignInCommand
{
    public class SignInCommandHandler : RequestHandlerBase<SignInCommand, Core.Models.Responses.User>
    {
        public SignInCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}