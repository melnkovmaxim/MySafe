using AutoMapper;
using Fody;
using MySafe.Core.Models.Requests;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Users.TwoFactorAuthenticationCommand
{
    [ConfigureAwait(false)]
    public class TwoFactorAuthenticationCommandHandler : RequestHandlerBase<TwoFactorAuthenticationCommand, UserJsonBody, UserEntity>
    {
        public TwoFactorAuthenticationCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}