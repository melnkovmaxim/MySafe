using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Users.SmsRequestCommand
{
    public class SmsRequestCommand : BearerRequestBase<UserEntity>, ITwoFactorRequest
    {
        public override Method RequestMethod => Method.POST;
        public override string RequestResource => "auth/sms-request";
    }
}
