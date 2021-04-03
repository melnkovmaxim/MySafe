using MySafe.Core.Entities.Responses;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Users.SignOutCommand
{
    /// <summary>
    ///     Выход
    /// </summary>
    public class SignOutCommand : BearerRequestBase<User>
    {
        public override Method RequestMethod => Method.DELETE;
        public override string RequestResource => "users/sign_out";
    }
}