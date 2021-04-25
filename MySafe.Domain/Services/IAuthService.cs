using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Domain.Services
{
    public interface IAuthService
    {
        Task<bool> IsExpiredAccessToken();
        Task<bool> IsExistsAccessToken();
        Task<bool> IsAuthorized();
        Task SignOut();
    }
}
