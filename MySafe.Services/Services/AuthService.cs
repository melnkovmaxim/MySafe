using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fody;
using MediatR;
using MySafe.Core.Interfaces.Repositories;
using MySafe.Core.Interfaces.Services;
using MySafe.Services.Extensions;
using MySafe.Services.Mediator.Users.RefreshTokenQuery;
using MySafe.Services.Mediator.Users.SignOutCommand;

namespace MySafe.Services.Services
{
    [ConfigureAwait(false)]
    public class AuthService: IAuthService
    {
        private readonly IMediator _mediator;
        private readonly ISecureStorageRepository _secureStorageRepository;
        private readonly IDeviceAuthService _deviceAuthService;

        public AuthService(IMediator mediator, ISecureStorageRepository secureStorageRepository, IDeviceAuthService deviceAuthService)
        {
            _mediator = mediator;
            _secureStorageRepository = secureStorageRepository;
            _deviceAuthService = deviceAuthService;
        }

        public async Task SignOut()
        {
            await _deviceAuthService.Logout();
            var refreshToken = await _secureStorageRepository.GetRefreshTokenAsync();

            if (string.IsNullOrEmpty(refreshToken))
            {
                var result = await _mediator.Send(new SignOutCommand(refreshToken));

                if (result.HasError)
                {
                    //TODO: Залогировать.

                    await _secureStorageRepository.RemoveAccessJwtAsync();
                    await _secureStorageRepository.RemoveRefreshTokenAsync();
                }
            }
        }

        public async Task<bool> IsAuthorized()
        {
            if (await IsExpiredAccessToken() == false) return true;
            if (await IsValidRefreshToken() == false) return false;

            var jwtRefreshToken = await _secureStorageRepository.GetRefreshTokenAsync();
            var result = await _mediator.Send(new RefreshTokenQuery(jwtRefreshToken));

            if (result.HasError)
            {
                //TODO: Залогировать.
                return false;
            }

            return true;
        }

        public async Task<bool> IsExistsAccessToken()
        {
            var accessToken = await _secureStorageRepository.GetAccessJwtAsync();

            return !string.IsNullOrEmpty(accessToken);
        }

        public async Task<bool> IsExpiredAccessToken()
        {
            var accessToken = await _secureStorageRepository.GetAccessSecurityTokenAsync();

            return accessToken == null || accessToken.IsExpired();
        }

        private async Task<bool> IsValidRefreshToken()
        {
            var refreshToken = await _secureStorageRepository.GetRefreshTokenAsync();

            return !string.IsNullOrEmpty(refreshToken);
        }
    }
}
