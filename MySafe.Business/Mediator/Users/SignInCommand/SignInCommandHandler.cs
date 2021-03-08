using Fody;
using MediatR;
using RestSharp;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Business.Extensions;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Requests;
using MySafe.Core.Entities.Responses;
using User = MySafe.Core.Entities.Responses.User;

namespace MySafe.Business.Mediator.Users.SignInCommand
{
    public class SignInCommandHandler : RequestHandlerBase<SignInCommand, MySafe.Core.Entities.Responses.User>
    {
        public SignInCommandHandler(IRestClient restClient, IMapper mapper): base(restClient, mapper)
        {
        }
    }
}
