using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using MySafe.Business.Mediator.Abstractions;
using MySafe.Business.Mediator.Users.TwoFactorAuthenticationCommand;
using MySafe.Core.Entities.Responses.Abstractions;
using MySafe.Data.Abstractions;

namespace MySafe.Business.Mediator
{
    public class BearerPreRequestHandler<TResponse>: IRequestPreProcessor<BearerRequestBase<TResponse>>
        where TResponse: IResponse
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
                twoFactorRequest.JwtToken = await _secureStorageRepository.GetJwtTokenForTwoFactorAsync();
                return;
            }

            request.JwtToken = await _secureStorageRepository.GetJwtTokenAsync();
        }
    }
}
