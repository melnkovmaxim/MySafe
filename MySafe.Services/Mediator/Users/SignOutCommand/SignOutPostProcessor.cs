using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using MySafe.Core.Models.Responses;
using MySafe.Domain.Repositories;

namespace MySafe.Services.Mediator.Users.SignOutCommand
{
    public class SignOutPostProcessor : IRequestPostProcessor<SignOutCommand, UserEntity>
    {
        private readonly ISecureStorageRepository _secureStorageRepository;

        public SignOutPostProcessor(ISecureStorageRepository secureStorageRepository)
        {
            _secureStorageRepository = secureStorageRepository;
        }

        public async Task Process(SignOutCommand request, UserEntity response, CancellationToken cancellationToken)
        {
            await _secureStorageRepository.RemoveJwtToken();
            await _secureStorageRepository.RemoveRefreshTokenAsync();
        }
    }
}