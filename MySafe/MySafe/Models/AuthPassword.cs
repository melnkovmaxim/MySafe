using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImTools;
using Prism.Mvvm;

namespace MySafe.Models
{
    public class AuthPassword : BindableBase
    {
        private Stack<int> Password;

        public bool IsSetNumber1 => Password.Count > 0;
        public bool IsSetNumber2 => Password.Count > 1;
        public bool IsSetNumber3 => Password.Count > 2;
        public bool IsSetNumber4 => Password.Count > 3;
        public bool IsSetNumber5 => Password.Count > 4;

        public AuthPassword()
        {
            Password = new Stack<int>();
        }

        public void Push(int value)
        {
            if (Password.Count < 5)
            {
                Password.Push(value);
                UpdateProperty();
            }
        }

        public void Pop()
        {
            if (Password.Count > 0)
            {
                Password.Pop();
                UpdateProperty();
            }
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
