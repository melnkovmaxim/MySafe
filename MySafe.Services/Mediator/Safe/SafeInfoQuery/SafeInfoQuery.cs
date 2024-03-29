﻿using MySafe.Core.Models.Responses;
using MySafe.Services.Mediator.Abstractions;
using RestSharp;

namespace MySafe.Services.Mediator.Safe.SafeInfoQuery
{
    /// <summary>
    ///     Получить информацию о сейфе
    /// </summary>
    public class SafeInfoQuery : BearerRequestBase<SafeEntity>
    {
        public override Method RequestMethod => Method.GET;
        public override string RequestResource => "api/v1/my_safe";
    }
}