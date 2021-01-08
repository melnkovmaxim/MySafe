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
        private readonly int _passwordLength;
        public ObservableCollection<string> PasswordStack { get; set; }

        public ObservableCollection<Func<bool>> IsSetNumbers { get; set; }

        public bool IsSetNumber1 => PasswordStack.Count > 0;
        public bool IsSetNumber2 => PasswordStack.Count > 1;
        public bool IsSetNumber3 => PasswordStack.Count > 2;
        public bool IsSetNumber4 => PasswordStack.Count > 3;
        public bool IsSetNumber5 => PasswordStack.Count > 4;

        public AuthPassword(int passwordLength)
        {
            _passwordLength = passwordLength;
            PasswordStack = new ObservableCollection<string>();
            for (var i = 0; i < 5; i++)
            {
                PasswordStack.Add(string.Empty);
            }
            IsSetNumbers = new ObservableCollection<Func<bool>>();

            for (var i = 0; i < _passwordLength; i ++)
            {
                IsSetNumbers.Add(() => true);
            }
        }

        public void Push(string value)
        {
            var @string = PasswordStack.FirstOrDefault(string.IsNullOrEmpty);

            if (@string != null)
            {
                var lastIndex = PasswordStack.IndexOf(@string);
                PasswordStack[lastIndex] = value;
            }
        }

        public void Pop()
        {
            var @string = PasswordStack.LastOrDefault(x => !string.IsNullOrEmpty(x));

            if (@string != null)
            {
                var lastIndex = PasswordStack.ToList().LastIndexOf(@string);
                PasswordStack[lastIndex] = string.Empty;
            }
        }

        public async Task<string> GetPassword()
        {
            var password = string.Join("", PasswordStack.Reverse());

            if (PasswordStack.Count == _passwordLength)
            {
                await Task.Run(() => Thread.Sleep(500));
                PasswordStack.Clear();
                UpdateProperty();
            }

            return password;
        }

        private void UpdateProperty()
        {
            RaisePropertyChanged(nameof(IsSetNumbers));
            foreach (var k in IsSetNumbers)
            {
                RaisePropertyChanged(nameof(k));
            }
            typeof(AuthPassword)
                .GetProperties()
                .Where(x => x.Name.StartsWith("IsSet"))
                .ForEach(x => RaisePropertyChanged(x.Name));
        }
    }
}
