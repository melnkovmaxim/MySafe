using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using MySafe.Core.Interfaces.Repositories;
using MySafe.Core.Models.Responses;

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
            await _secureStorageRepository.RemoveAccessJwtAsync();
            await _secureStorageRepository.RemoveRefreshTokenAsync();
        }
    }
}