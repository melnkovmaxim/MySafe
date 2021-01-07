using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ImTools;
using Prism.Mvvm;

namespace MySafe.Models
{
    public class AuthPassword : BindableBase
    {
        private Stack<int> PasswordStack;

        public bool IsSetNumber1 => PasswordStack.Count > 0;
        public bool IsSetNumber2 => PasswordStack.Count > 1;
        public bool IsSetNumber3 => PasswordStack.Count > 2;
        public bool IsSetNumber4 => PasswordStack.Count > 3;
        public bool IsSetNumber5 => PasswordStack.Count > 4;

        public AuthPassword()
        {
            PasswordStack = new Stack<int>();
        }

        public void Push(int value)
        {
            if (PasswordStack.Count < 5)
            {
                PasswordStack.Push(value);
                UpdateProperty();
            }
        }

        public void Pop()
        {
            if (PasswordStack.Count > 0)
            {
                PasswordStack.Pop();
                UpdateProperty();
            }
        }

        public async Task<string> GetPassword()
        {
            var password = string.Join("", PasswordStack.Reverse());

            if (PasswordStack.Count == 5)
            {
                await Task.Run(() => Thread.Sleep(1000));
                PasswordStack.Clear();
                UpdateProperty();
            }

            return password;
        }

        private void UpdateProperty()
        {
            RaisePropertyChanged(nameof(IsSetNumber1));
            RaisePropertyChanged(nameof(IsSetNumber2));
            RaisePropertyChanged(nameof(IsSetNumber3));
            RaisePropertyChanged(nameof(IsSetNumber4));
            RaisePropertyChanged(nameof(IsSetNumber5));
        }
    }
}
