using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Core.Entities;

namespace MySafe.Business.Extensions
{
    public static class ApplicationPasswordExtensions
    {
        public static void SetPasswordLength(this ApplicationPassword password, int length)
        {
            var passwordCollection = password.PasswordCollection;

            passwordCollection.Clear();

            for (var i = 0; i < length; i++)
            {
                passwordCollection.Add(string.Empty);
            }
        }

        public static bool TryAdd(this ApplicationPassword password, string value)
        {
            if (password.PasswordLength == password.PasswordMaxLength)
                return false;

            var passwordCollection = password.PasswordCollection;
            var lastIndex = passwordCollection.IndexOf(string.Empty);
            passwordCollection[lastIndex] = value;

            return true;
        }

        public static void RemoveLast(this ApplicationPassword password)
        {
            var passwordCollection = password.PasswordCollection;
            var @string = passwordCollection.LastOrDefault(x => !string.IsNullOrEmpty(x));

            if (@string != null)
            {
                var lastIndex = passwordCollection.ToList().LastIndexOf(@string);
                passwordCollection[lastIndex] = string.Empty;
            }
        }

        public static void Clear(this ApplicationPassword password)
        {
            for (var i = 0; i < password.PasswordMaxLength; i++)
            {
                password.PasswordCollection[i] = string.Empty;
            }
        }
    }
}
