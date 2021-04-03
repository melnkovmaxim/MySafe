using Fody;
using MySafe.Core.Entities.Responses;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Users.TwoFactorAuthenticationCommand
{
    [ConfigureAwait(false)]
    public class TwoFactorAuthenticationCommandHandler : RequestHandlerBase<TwoFactorAuthenticationCommand, User>
    {
        public TwoFactorAuthenticationCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}