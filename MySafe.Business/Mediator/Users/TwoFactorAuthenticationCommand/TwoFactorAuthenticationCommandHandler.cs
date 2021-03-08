using Fody;
using MediatR;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Business.Extensions;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Requests;
using MySafe.Core.Entities.Responses;
using MySafe.Data.Abstractions;
using User = MySafe.Core.Entities.Responses.User;

namespace MySafe.Business.Mediator.Users.TwoFactorAuthenticationCommand
{
    [ConfigureAwait(false)]
    public class TwoFactorAuthenticationCommandHandler : RequestHandlerBase<TwoFactorAuthenticationCommand, User>
    {
        public TwoFactorAuthenticationCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}
