using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Domain.Services
{
    public interface IJwtService
    {
        Task<bool> IsExpiredJwtTokensAsync();
    }
}
