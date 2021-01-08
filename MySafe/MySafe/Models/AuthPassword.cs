using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ImTools;
using Prism.Mvvm;
using Xamarin.Forms.Internals;

namespace MySafe.Models
{
    // TODO: Убрать из моделей, решить вопрос с RaiseNotify и большим количеством свойств
    public class AuthPassword : BindableBase
    {
        public ObservableCollection<string> Password { get; }
        public int PasswordLength => Password.Count;

        public AuthPassword(int passwordLength)
        {
            Password = new ObservableCollection<string>();

            for (var i = 0; i < passwordLength; i++)
            {
                Password.Add(string.Empty);
            }
        }

        public void Add(string value)
        {
            var @string = Password.FirstOrDefault(string.IsNullOrEmpty);

            if (@string != null)
            {
                var lastIndex = Password.IndexOf(@string);
                Password[lastIndex] = value;
            }
        }

        public void RemoveLast()
        {
            var @string = Password.LastOrDefault(x => !string.IsNullOrEmpty(x));

            if (@string != null)
            {
                var lastIndex = Password.ToList().LastIndexOf(@string);
                Password[lastIndex] = string.Empty;
            }
        }

        public async Task<string> GetPassword()
        {
            var password = string.Join("", Password.Reverse());

            if (Password.Count == PasswordLength)
            {
                await Task.Run(() => Thread.Sleep(250));
                Password.Clear();
            }

            return password;
        }
    }
}
