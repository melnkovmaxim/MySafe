using MediatR.Pipeline;
using MySafe.Core.Interfaces.Repositories;
using MySafe.Core.Models.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace MySafe.Services.Mediator.Users.RefreshTokenQuery
{
    public class RefreshTokenQueryPostProcessor : IRequestPostProcessor<RefreshTokenQuery, UserEntity>
    {
        private readonly ISecureStorageRepository _secureStorageRepository;

        public RefreshTokenQueryPostProcessor(ISecureStorageRepository secureStorageRepository)
        {
            _secureStorageRepository = secureStorageRepository;
        }

        public Task Process(RefreshTokenQuery request, UserEntity response, CancellationToken cancellationToken)
        {
            _secureStorageRepository.SetAccessJwtAsync(response.JwtToken ?? string.Empty);
            _secureStorageRepository.SetRefreshTokenAsync(response.RefreshToken ?? string.Empty);

            return Task.CompletedTask;
        }
    }
}
