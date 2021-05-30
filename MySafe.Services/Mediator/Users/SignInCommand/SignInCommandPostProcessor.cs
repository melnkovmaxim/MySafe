using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using MySafe.Core;
using MySafe.Core.Interfaces.Repositories;
using MySafe.Core.Models.Responses;

namespace MySafe.Services.Mediator.Users.SignInCommand
{
    public class SignInCommandPostProcessor : IRequestPostProcessor<SignInCommand, UserEntity>
    {
        private readonly ISecureStorageRepository _secureStorageRepository;

        public SignInCommandPostProcessor(ISecureStorageRepository secureStorageRepository)
        {
            _secureStorageRepository = secureStorageRepository;
        }

        public async Task Process(SignInCommand request, UserEntity response,
            CancellationToken cancellationToken)
        {
            if (!response.HasError)
            {
                await _secureStorageRepository.SetTwoFactorJwtAsync(response.JwtToken);
                await _secureStorageRepository.SetRefreshTokenAsync(response.RefreshToken);
                await _secureStorageRepository.SetUserLogin(request.Login);
                MySafeApp.Resources.UserLogin = request.Login;
            }
        }
    }
}