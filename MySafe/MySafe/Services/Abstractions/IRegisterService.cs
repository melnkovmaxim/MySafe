using System;
using Prism.Navigation;
using System.Threading.Tasks;

namespace MySafe.Services.Abstractions
{
    public interface IRegisterService
    {
        Task RegisterAsync(string password, int requiredLength, Action actionOnRegister);
    }
}
