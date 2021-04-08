using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using MySafe.Core.Models.Responses;
using MySafe.Domain.Repositories;

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
            //TODO: Посмотреть сет резалт с сервера и передавать токен
            _secureStorageRepository.SetRefreshTokenAsync(null);

            return Task.CompletedTask;
        }
    }
}
