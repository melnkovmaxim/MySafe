﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using MySafe.Core.Entities.Responses;
using MySafe.Data.Abstractions;

namespace MySafe.Business.Mediator.Users.TwoFactorAuthenticationCommand
{
    public class TwoFactorAuthenticationPostProcessor: IRequestPostProcessor<TwoFactorAuthenticationCommand, User>
    {
        private readonly ISecureStorageRepository _secureStorageRepository;

        public TwoFactorAuthenticationPostProcessor(ISecureStorageRepository secureStorageRepository)
        {
            _secureStorageRepository = secureStorageRepository;
        }

        public Task Process(TwoFactorAuthenticationCommand request, User response, CancellationToken cancellationToken)
        {
            return _secureStorageRepository.SetJwtTokenAsync(response.JwtToken);
        }
    }
}
