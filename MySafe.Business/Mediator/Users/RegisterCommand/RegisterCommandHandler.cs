using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MySafe.Business.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Business.Mediator.Users.RegisterCommand
{
    public class RegisterCommandHandler : RequestHandlerBase<RegisterCommand, MySafe.Core.Entities.Responses.User>
    {
        public RegisterCommandHandler(IRestClient restClient, IMapper mapper) : base(restClient, mapper)
        {
        }
    }
}
