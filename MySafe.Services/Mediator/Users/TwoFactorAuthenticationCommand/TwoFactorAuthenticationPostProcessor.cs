﻿using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using MySafe.Core.Interfaces.Repositories;
using MySafe.Core.Models.Responses;

namespace MySafe.Services.Mediator.Users.TwoFactorAuthenticationCommand
{
    public class
        TwoFactorAuthenticationPostProcessor : IRequestPostProcessor<TwoFactorAuthenticationCommand, UserEntity>
    {
        private readonly ISecureStorageRepository _secureStorageRepository;

        public TwoFactorAuthenticationPostProcessor(ISecureStorageRepository secureStorageRepository)
        {
            _secureStorageRepository = secureStorageRepository;
        }

        public async Task Process(TwoFactorAuthenticationCommand request, UserEntity response,
            CancellationToken cancellationToken)
        {
            if (!response.HasError)
            {
                await _secureStorageRepository.RemoveTwoFactorJwtAsync();
                await _secureStorageRepository.SetAccessJwtAsync(response.JwtToken);
            }
        }
    }
}