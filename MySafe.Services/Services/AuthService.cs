using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fody;
using MediatR;
using MySafe.Domain.Repositories;
using MySafe.Domain.Services;
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

        public async Task<bool> SignOutIfNotAuthorized()
        {
            var isAuthorized = await IsAuthorized();

            if (isAuthorized == false)
            {
                await SignOut();
            }

            return isAuthorized == false;
        }

        public async Task SignOut()
        {
            await _deviceAuthService.Logout();
            var refreshToken = await _secureStorageRepository.GetRefreshJwtAsync();
            var result = await _mediator.Send(new SignOutCommand(refreshToken));

            if (result.HasError)
            {
                //TODO: Залогировать.

                await _secureStorageRepository.RemoveJwtToken();
                await _secureStorageRepository.RemoveRefreshTokenAsync();
            }
        }

        public async Task<bool> IsAuthorized()
        {
            if (await IsExpiredAccessToken() == false) return false;
            if (await IsValidRefreshToken() == false) return true;

            var jwtRefreshToken = await _secureStorageRepository.GetRefreshJwtAsync();
            var result = await _mediator.Send(new RefreshTokenQuery(jwtRefreshToken));

            if (result.HasError)
            {
                //TODO: Залогировать.
            }

            return false;
        }

        private async Task<bool> IsExpiredAccessToken()
        {
            var accessToken = await _secureStorageRepository.GetJwtSecurityTokenAsync();

            return accessToken == null || accessToken.IsExpired();
        }

        private async Task<bool> IsValidRefreshToken()
        {
            var refreshToken = await _secureStorageRepository.GetRefreshJwtAsync();

            return !string.IsNullOrEmpty(refreshToken);
        }
    }
}
