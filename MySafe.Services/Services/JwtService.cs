using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MySafe.Domain.Repositories;
using MySafe.Domain.Services;
using MySafe.Services.Extensions;
using MySafe.Services.Mediator.Users.RefreshTokenQuery;

namespace MySafe.Services.Services
{
    public class JwtService: IJwtService
    {
        private readonly IMediator _mediator;
        private readonly ISecureStorageRepository _secureStorageRepository;

        public JwtService(IMediator mediator, ISecureStorageRepository secureStorageRepository)
        {
            _mediator = mediator;
            _secureStorageRepository = secureStorageRepository;
        }

        public async Task<bool> IsExpiredJwtTokensAsync()
        {
            if (await IsExpiredAccessToken()) return true;
            if (await IsExpiredRefreshToken()) return true;

            var jwtRefreshToken = await _secureStorageRepository.GetRefreshJwtAsync();
            var result = await _mediator.Send(new RefreshTokenQuery(jwtRefreshToken));

            if (result.HasError)
            {
                throw new Exception(result.Error);
            }

            return false;
        }

        private async Task<bool> IsExpiredAccessToken()
        {
            var accessToken = await _secureStorageRepository.GetJwtSecurityTokenAsync();

            return accessToken == null || accessToken.IsExpired();
        }

        //TODO: насколько понял это не JWT токен у них, поэтому стоит переименовать метод, т.к. он вечный
        private async Task<bool> IsExpiredRefreshToken()
        {
            var refreshToken = await _secureStorageRepository.GetRefreshJwtAsync();

            return string.IsNullOrEmpty(refreshToken);
        }

    }
}
