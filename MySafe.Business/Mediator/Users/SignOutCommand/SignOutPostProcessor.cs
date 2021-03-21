using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using MySafe.Core.Entities.Responses;
using MySafe.Data.Abstractions;

namespace MySafe.Business.Mediator.Users.SignOutCommand
{
    public class SignOutPostProcessor: IRequestPostProcessor<SignOutCommand, User>
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
