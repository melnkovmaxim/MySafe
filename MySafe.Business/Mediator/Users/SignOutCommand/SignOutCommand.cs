using MediatR;
using System.IdentityModel.Tokens.Jwt;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Core.Entities.Responses;
using RestSharp;

namespace MySafe.Business.Mediator.Users.SignOutCommand
{
    /// <summary>
    /// Выход
    /// </summary>
    public class SignOutCommand : BearerRequestBase<User>
    {
        public SignOutCommand(string jwtToken) : base(jwtToken)
        {
        }

        public override Method RequestMethod => Method.DELETE;
        public override string RequestResource => "users/sign_out";
    }
}
