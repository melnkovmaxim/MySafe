#pragma warning disable 612,618
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MySafe.Core.Commands
{
    public abstract class CommandBase
    {
        public static bool DefaultUseCommandManager { get; set; } = true;
    }

    public interface ICommand<T> : ICommand
    {
        void Execute(T param);
        bool CanExecute(T param);
    }

    public abstract class CommandBase<T> : CommandBase, ICommand<T>, IDelegateCommand
    {
        protected Func<T, bool> canExecuteMethod;

        public event EventHandler CanExecuteChanged
        {
            add => canExecuteChanged += value;
            remove => canExecuteChanged -= value;
        }


        public virtual bool CanExecute(T parameter)
        {
            if (canExecuteMethod == null) return true;
            return canExecuteMethod(parameter);
        }

        public abstract void Execute(T parameter);

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(GetGenericParameter(parameter, true));
        }

        void ICommand.Execute(object parameter)
        {
            Execute(GetGenericParameter(parameter));
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        private event EventHandler canExecuteChanged;

        protected virtual void OnCanExecuteChanged()
        {
            if (canExecuteChanged != null)
                canExecuteChanged(this, EventArgs.Empty);
        }

        internal static T GetGenericParameter(object parameter, bool suppressCastException = false)
        {
            parameter = TypeCastHelper.TryCast(parameter, typeof(T));
            if (parameter == null || parameter is T) return (T) parameter;
            if (suppressCastException) return default;
            throw new InvalidCastException(string.Format(
                "CommandParameter: Unable to cast object of type '{0}' to type '{1}'", parameter.GetType().FullName,
                typeof(T).FullName));
        }
    }

    public abstract class DelegateCommandBase<T> : CommandBase<T>
    {
        protected Action<T> executeMethod;

        public DelegateCommandBase(Action<T> executeMethod)
            : this(executeMethod, null)
        {
        }

        public DelegateCommandBase(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            Init(executeMethod, canExecuteMethod);
        }

        private void Init(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            if (executeMethod == null && canExecuteMethod == null)
                throw new ArgumentNullException("executeMethod");
            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }
    }

    public abstract class AsyncCommandBase<T> : CommandBase<T>, INotifyPropertyChanged
    {
        protected Func<T, Task> executeMethod;

        public AsyncCommandBase(Func<T, Task> executeMethod)
            : this(executeMethod, null)
        {
        }

        public AsyncCommandBase(Func<T, Task> executeMethod, bool useCommandManager)
            : this(executeMethod, null)
        {
        }

        public AsyncCommandBase(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod)
        {
            Init(executeMethod, canExecuteMethod);
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add => propertyChanged += value;
            remove => propertyChanged -= value;
        }

        private void Init(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod)
        {
            if (executeMethod == null && canExecuteMethod == null)
                throw new ArgumentNullException("executeMethod");
            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        private event PropertyChangedEventHandler propertyChanged;

        protected void RaisePropertyChanged(string propName)
        {
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }

    public class Command<T> : DelegateCommandBase<T>
    {
        public Command(Action<T> executeMethod)
            : this(executeMethod, null)
        {
        }

        public Command(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
            : base(executeMethod, canExecuteMethod)
        {
        }

        public override void Execute(T parameter)
        {
            if (!CanExecute(parameter))
                return;
            if (executeMethod == null) return;
            executeMethod(parameter);
        }
    }

    public class DelegateCommand : Command<object>
    {
        public DelegateCommand(Action executeMethod)
            : this(executeMethod, null)
        {
        }

        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
            : base(
                executeMethod != null ? (Action<object>) (o => executeMethod()) : null,
                canExecuteMethod != null ? (Func<object, bool>) (o => canExecuteMethod()) : null)
        {
        }
    }

    public class AsyncCommand<T> : AsyncCommandBase<T>, IAsyncCommand
    {
        private CancellationTokenSource cancellationTokenSource;
        internal Task executeTask;
        private bool isExecuting;
        private bool isLegacyExecuting;
        private bool shouldCancel;

        public AsyncCommand(Func<T, Task> executeMethod)
            : this(executeMethod, null, false)
        {
        }

        public AsyncCommand(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod)
            : this(executeMethod, canExecuteMethod, false)
        {
        }

        public AsyncCommand(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod, bool allowMultipleExecution)
            : base(executeMethod, canExecuteMethod)
        {
            CancelCommand = new DelegateCommand(Cancel, CanCancel);
            AllowMultipleExecution = allowMultipleExecution;
        }

        public bool AllowMultipleExecution { get; set; }

        public DelegateCommand CancelCommand { get; }

        public bool IsExecuting
        {
            get => isExecuting;
            private set
            {
                if (isExecuting == value) return;
                isExecuting = value;
                RaisePropertyChanged(BindableBase.GetPropertyName(() => IsExecuting));
                OnIsExecutingChanged();
            }
        }

        public CancellationTokenSource CancellationTokenSource
        {
            get => cancellationTokenSource;
            private set
            {
                if (cancellationTokenSource == value) return;
                cancellationTokenSource = value;
                RaisePropertyChanged(BindableBase.GetPropertyName(() => CancellationTokenSource));
            }
        }

        [Obsolete("Use the IsCancellationRequested property instead.")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldCancel
        {
            get => shouldCancel;
            private set
            {
                if (shouldCancel == value) return;
                shouldCancel = value;
                RaisePropertyChanged(BindableBase.GetPropertyName(() => ShouldCancel));
            }
        }

        public bool IsCancellationRequested
        {
            get
            {
                if (CancellationTokenSource == null) return false;
                return CancellationTokenSource.IsCancellationRequested;
            }
        }

        ICommand IAsyncCommand.CancelCommand => CancelCommand;

        Task IAsyncCommand.ExecuteAsync(object parameter)
        {
            return ExecuteAsync(GetGenericParameter(parameter));
        }

        public override bool CanExecute(T parameter)
        {
            if (!AllowMultipleExecution && IsExecuting) return false;
            return base.CanExecute(parameter);
        }

        public override void Execute(T parameter)
        {
            ExecuteCommon(parameter);
        }

        public Task ExecuteAsync(T parameter)
        {
            return ExecuteCommon(parameter);
        }

        private Task ExecuteCommon(T parameter)
        {
            if (!CanExecute(parameter) || executeMethod == null)
                return Task.FromResult<object>(null);
            IsExecuting = true;
            CancellationTokenSource = new CancellationTokenSource();
            return ExecuteCore(parameter);
        }

        private Task ExecuteCore(T parameter)
        {
            executeTask = executeMethod(parameter).ContinueWith(x =>
            {
                IsExecuting = false;
                ShouldCancel = false;
                if (x.IsFaulted)
                    throw x.Exception.InnerException;
            }, TaskScheduler.FromCurrentSynchronizationContext());
            return executeTask;
        }
#if DEBUG
        [Obsolete("Use CancellationTokenSource.Cancel instead.")]
#endif
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Cancel()
        {
            if (!CanCancel()) return;
            ShouldCancel = true;
            CancellationTokenSource.Cancel();
        }

        private bool CanCancel()
        {
            return IsExecuting;
        }

        private void OnIsExecutingChanged()
        {
            CancelCommand.RaiseCanExecuteChanged();
            RaiseCanExecuteChanged();
        }
    }

    public class AsyncCommand : AsyncCommand<object>
    {
        public AsyncCommand(Func<Task> executeMethod)
            : this(executeMethod, null, false)
        {
        }

        public AsyncCommand(Func<Task> executeMethod, Func<bool> canExecuteMethod)
            : this(executeMethod, canExecuteMethod, false)
        {
        }

        public AsyncCommand(Func<Task> executeMethod, Func<bool> canExecuteMethod, bool allowMultipleExecution)
            : base(
                executeMethod != null ? (Func<object, Task>) (o => executeMethod()) : null,
                canExecuteMethod != null ? (Func<object, bool>) (o => canExecuteMethod()) : null,
                allowMultipleExecution)
        {
        }
    }
}
#pragma warning restore 612, 618