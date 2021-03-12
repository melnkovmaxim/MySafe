﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using MySafe.Data.Abstractions;

namespace MySafe.Business.Mediator.Users.SignInCommand
{
    public class SignInCommandPostProcessor: IRequestPostProcessor<SignInCommand, MySafe.Core.Entities.Responses.User>
    {
        private readonly ISecureStorageRepository _secureStorageRepository;

        public SignInCommandPostProcessor(ISecureStorageRepository secureStorageRepository)
        {
            _secureStorageRepository = secureStorageRepository;
        }

        public Task Process(SignInCommand request, Core.Entities.Responses.User response, CancellationToken cancellationToken)
        {
            return _secureStorageRepository.SetJwtTokenForTwoFactorAsync(response.JwtToken);
        }
    }
}
