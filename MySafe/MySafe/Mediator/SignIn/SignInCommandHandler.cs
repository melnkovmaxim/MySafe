using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MySafe.Mediator.SignIn
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, string>
    {
        public async Task<string> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            await Task.Run(() => Thread.Sleep(1000), cancellationToken);
            return "HELLO!";
        }
    }
}
