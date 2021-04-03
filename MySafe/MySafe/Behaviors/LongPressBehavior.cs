using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MySafe.Presentation.Behaviors
{
    public class LongPressBehavior : Behavior<Button>
    {
        private static readonly object _syncObject = new object();

        private Timer _timer;
        private volatile bool _isReleased;
        private object _context;

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(LongPressBehavior), default(ICommand));
        public static readonly BindableProperty EarlyReleaseCommandProperty = BindableProperty.Create(nameof(EarlyReleaseCommand), typeof(ICommand), typeof(LongPressBehavior), default(ICommand));
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(LongPressBehavior));
        public static readonly BindableProperty DurationProperty = BindableProperty.Create(nameof(Duration), typeof(int), typeof(LongPressBehavior), 1000);

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public ICommand EarlyReleaseCommand
        {
            get => (ICommand)GetValue(EarlyReleaseCommandProperty);
            set => SetValue(EarlyReleaseCommandProperty, value);
        }

        public int Duration
        {
            get => (int)GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        protected override void OnAttachedTo(Button bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Pressed += Button_Pressed;
            bindable.Released += Button_Released;
        }

        protected override void OnDetachingFrom(Button bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.Pressed -= Button_Pressed;
            bindable.Released -= Button_Released;
        }

        private void DeInitializeTimer()
        {
            lock (_syncObject)
            {
                if (_timer == null)
                {
                    return;
                }
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
                _timer.Dispose();
                _timer = null;
            }
        }

        private void InitializeTimer()
        {
            lock (_syncObject)
            {
                _timer = new Timer(Timer_Elapsed, null, Duration, Timeout.Infinite);
            }
        }

        private void Button_Pressed(object sender, EventArgs e)
        {
            _isReleased = false;
            _context = (sender as ImageButton)?.BindingContext;
            InitializeTimer();
        }

        private void Button_Released(object sender, EventArgs e)
        {
            if (!_isReleased)
            {
                EarlyReleaseCommand?.Execute(CommandParameter);
            }
            _isReleased = true;
            _context = null;
            DeInitializeTimer();
        }

        protected virtual void OnLongPressed()
        {
            if (Command != null && Command.CanExecute(CommandParameter))
            {
                if (CommandParameter == null && Command.CanExecute(_context))
                {
                    Command.Execute(_context);
                    return;
                }
                Command.Execute(CommandParameter);
            }
        }

        public LongPressBehavior()
        {
            _isReleased = true;
        }

        private void Timer_Elapsed(object state)
        {
            DeInitializeTimer();
            if (_isReleased)
            {
                return;
            }
            _isReleased = true;
            Device.BeginInvokeOnMainThread(OnLongPressed);
        }
    }
}
