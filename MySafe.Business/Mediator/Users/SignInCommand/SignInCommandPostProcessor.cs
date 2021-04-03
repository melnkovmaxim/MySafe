using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using MySafe.Core;
using MySafe.Domain.Repositories;

namespace MySafe.Services.Mediator.Users.SignInCommand
{
    public class SignInCommandPostProcessor : IRequestPostProcessor<SignInCommand, Core.Models.Responses.User>
    {
        private readonly ISecureStorageRepository _secureStorageRepository;

        public SignInCommandPostProcessor(ISecureStorageRepository secureStorageRepository)
        {
            _secureStorageRepository = secureStorageRepository;
        }

        public async Task Process(SignInCommand request, Core.Models.Responses.User response,
            CancellationToken cancellationToken)
        {
            if (!response.HasError)
            {
                await _secureStorageRepository.SetJwtTokenForTwoFactorAsync(response.JwtToken);
                await _secureStorageRepository.SetUserLogin(request.User.Login);
                MySafeApp.Resources.UserLogin = request.User.Login;
            }
        }
    }
}