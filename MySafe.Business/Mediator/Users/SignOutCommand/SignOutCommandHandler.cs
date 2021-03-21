using Fody;
using MediatR;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Business.Extensions;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using MySafe.Data.Abstractions;

namespace MySafe.Business.Mediator.Users.SignOutCommand
{
    public class SignOutCommandHandler : RequestHandlerBase<SignOutCommand, User>
    {
        public SignOutCommandHandler(IRestClient restClient) : base(restClient)
        {
        }
    }
}
