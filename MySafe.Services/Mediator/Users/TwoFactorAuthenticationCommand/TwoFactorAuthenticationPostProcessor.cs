using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using MySafe.Core.Models.Responses;
using MySafe.Domain.Repositories;

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
                await _secureStorageRepository.RemoveTwoFactorJwtToken();
                await _secureStorageRepository.SetJwtTokenAsync(response.JwtToken);
            }
        }
    }
}