using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using MySafe.Core.Entities.Abstractions;
using MySafe.Domain.Repositories;
using MySafe.Services.Mediator.Abstractions;
using MySafe.Services.Mediator.Users.TwoFactorAuthenticationCommand;

namespace MySafe.Services.Mediator
{
    public class BearerPreRequestHandler<TResponse> : IRequestPreProcessor<BearerRequestBase<TResponse>>
        where TResponse : IEntity
    {
        private readonly ISecureStorageRepository _secureStorageRepository;

        public BearerPreRequestHandler(ISecureStorageRepository secureStorageRepository)
        {
            _secureStorageRepository = secureStorageRepository;
        }

        public async Task Process(BearerRequestBase<TResponse> request, CancellationToken cancellationToken)
        {
            if (request is TwoFactorAuthenticationCommand twoFactorRequest)
            {
                twoFactorRequest.JwtToken = await _secureStorageRepository.GetTwoFactorJwtAsync();
                return;
            }

            request.JwtToken = await _secureStorageRepository.GetAccessJwtAsync();
        }
    }
}