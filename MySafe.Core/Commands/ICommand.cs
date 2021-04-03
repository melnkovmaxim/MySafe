using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using MySafe.Core.Commands;

namespace MySafe.Core.Commands
{
    public interface IDelegateCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }

    public interface IAsyncCommand : IDelegateCommand
    {
        bool IsExecuting { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use the IsCancellationRequested property instead.")]
        bool ShouldCancel { get; }

        CancellationTokenSource CancellationTokenSource { get; }
        bool IsCancellationRequested { get; }
        ICommand CancelCommand { get; }
#if DEBUG
        [Obsolete("Use 'await ExecuteAsync' instead.")]
#endif
        [EditorBrowsable(EditorBrowsableState.Never)]
        Task ExecuteAsync(object parameter);
    }
}

namespace DevExpress.Mvvm
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class IAsyncCommandExtensions
    {
        private static void VerifyService(IAsyncCommand service)
        {
            if (service == null) throw new ArgumentNullException("service");
        }
    }
}