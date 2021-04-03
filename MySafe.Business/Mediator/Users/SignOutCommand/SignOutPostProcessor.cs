using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using MySafe.Core.Entities.Responses;
using MySafe.Core.Models.Responses;
using MySafe.Domain.Repositories;

namespace MySafe.Services.Mediator.Users.SignOutCommand
{
    public class SignOutPostProcessor : IRequestPostProcessor<SignOutCommand, User>
    {
        private readonly ISecureStorageRepository _secureStorageRepository;

        public SignOutPostProcessor(ISecureStorageRepository secureStorageRepository)
        {
            _secureStorageRepository = secureStorageRepository;
        }

        public Task Process(SignOutCommand request, User response, CancellationToken cancellationToken)
        {
            return response.HasError ? Task.CompletedTask : _secureStorageRepository.RemoveJwtToken();
        }
    }
}