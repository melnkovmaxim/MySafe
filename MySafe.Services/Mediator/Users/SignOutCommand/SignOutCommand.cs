using MySafe.Core;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Users.SignOutCommand
{
    /// <summary>
    ///     Выход
    /// </summary>
    public class SignOutCommand : BearerRequestBase<UserEntity>
    {
        public string RefreshToken { get; set; }

        public SignOutCommand(string refreshToken)
        {
            RefreshToken = refreshToken;
        }

        public override Method RequestMethod => Method.POST;
        public override string RequestResource => "auth/sign-out";
    }
}