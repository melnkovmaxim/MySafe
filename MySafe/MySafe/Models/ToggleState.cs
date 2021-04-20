using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Presentation.Enums;
using Prism.Mvvm;
using Xamarin.Forms.Internals;

namespace MySafe.Presentation.Models
{
    public class ToggleState: BindableBase
    {
        private readonly Dictionary<ToggleStateEnum, Action> _rules;
        public ToggleStateEnum CurrentState { get; private set; }

        public ToggleState(ToggleStateEnum defaultState = ToggleStateEnum.Off)
        {
            CurrentState = defaultState;
            _rules = new Dictionary<ToggleStateEnum, Action>()
            {
                {ToggleStateEnum.On, () => CurrentState = ToggleStateEnum.Off},
                {ToggleStateEnum.Off, () => CurrentState = ToggleStateEnum.On}
            };
        }

        public void Toggle()
        {
            _rules[CurrentState].Invoke();
        }
    }
}
