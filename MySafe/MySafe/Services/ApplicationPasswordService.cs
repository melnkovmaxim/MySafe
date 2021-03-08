using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Business.Extensions;
using MySafe.Core;
using MySafe.Core.Entities;

namespace MySafe.Presentation.Services
{
    public class ApplicationPasswordService
    {
        public ApplicationPassword GetPasswordWithDefaultLength()
        {
            var password = new ApplicationPassword();
            
            password.SetPasswordLength(MySafeApp.Resources.DefaultApplicationPasswordLength);

            return password;
        }
    }
}
