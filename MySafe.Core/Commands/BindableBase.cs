using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MySafe.Core.Commands
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static string GetPropertyName<T>(Expression<Func<T>> expression)
        {
            return GetPropertyNameFast(expression);
        }

        internal static string GetPropertyNameFast(LambdaExpression expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new ArgumentException("MemberExpression is expected in expression.Body", "expression");
            const string vblocalPrefix = "$VB$Local_";
            var member = memberExpression.Member;
            if (
                member.MemberType == MemberTypes.Field &&
                member.Name != null &&
                member.Name.StartsWith(vblocalPrefix))
                return member.Name.Substring(vblocalPrefix.Length);
            return member.Name;
        }

        protected T GetProperty<T>(Expression<Func<T>> expression)
        {
            return GetPropertyCore<T>(GetPropertyName(expression));
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, string propertyName, Action changedCallback)
        {
            VerifyAccess();
            if (CompareValues(storage, value))
                return false;
            storage = value;
            RaisePropertyChanged(propertyName);
            changedCallback?.Invoke();
            return true;
        }

        protected bool SetProperty<T>(ref T storage, T value, Expression<Func<T>> expression, Action changedCallback)
        {
            return SetProperty(ref storage, value, GetPropertyName(expression), changedCallback);
        }

        protected bool SetProperty<T>(ref T storage, T value, Expression<Func<T>> expression)
        {
            return SetProperty(ref storage, value, expression, null);
        }

        protected bool SetProperty<T>(ref T storage, T value, string propertyName)
        {
            return SetProperty(ref storage, value, propertyName, null);
        }

        protected T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            GuardPropertyName(propertyName);
            return GetPropertyCore<T>(propertyName);
        }

        protected bool SetValue<T>(T value, [CallerMemberName] string propertyName = null)
        {
            return SetValue(value, default(Action), propertyName);
        }

        protected bool SetValue<T>(T value, Action changedCallback, [CallerMemberName] string propertyName = null)
        {
            return SetPropertyCore(propertyName, value, changedCallback);
        }

        protected bool SetValue<T>(T value, Action<T> changedCallback, [CallerMemberName] string propertyName = null)
        {
            return SetPropertyCore(propertyName, value, changedCallback);
        }

        protected bool SetValue<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            return SetValue(ref storage, value, default, propertyName);
        }

        protected bool SetValue<T>(ref T storage, T value, Action changedCallback,
            [CallerMemberName] string propertyName = null)
        {
            GuardPropertyName(propertyName);
            return SetProperty(ref storage, value, propertyName, changedCallback);
        }

        private static void GuardPropertyName(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));
        }

        protected bool SetProperty<T>(Expression<Func<T>> expression, T value)
        {
            return SetProperty(expression, value, (Action) null);
        }

        protected bool SetProperty<T>(Expression<Func<T>> expression, T value, Action changedCallback)
        {
            var propertyName = GetPropertyName(expression);
            return SetPropertyCore(propertyName, value, changedCallback);
        }

        protected bool SetProperty<T>(Expression<Func<T>> expression, T value, Action<T> changedCallback)
        {
            var propertyName = GetPropertyName(expression);
            return SetPropertyCore(propertyName, value, changedCallback);
        }

        #region RaisePropertyChanged

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RaisePropertyChanged()
        {
            RaisePropertiesChanged(null);
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> expression)
        {
            RaisePropertyChanged(GetPropertyName(expression));
        }

        protected void RaisePropertiesChanged(params string[] propertyNames)
        {
            if (propertyNames == null || propertyNames.Length == 0)
            {
                RaisePropertyChanged(string.Empty);
                return;
            }

            foreach (var propertyName in propertyNames) RaisePropertyChanged(propertyName);
        }

        protected void RaisePropertiesChanged<T1, T2>(Expression<Func<T1>> expression1,
            Expression<Func<T2>> expression2)
        {
            RaisePropertyChanged(expression1);
            RaisePropertyChanged(expression2);
        }

        protected void RaisePropertiesChanged<T1, T2, T3>(Expression<Func<T1>> expression1,
            Expression<Func<T2>> expression2, Expression<Func<T3>> expression3)
        {
            RaisePropertyChanged(expression1);
            RaisePropertyChanged(expression2);
            RaisePropertyChanged(expression3);
        }

        protected void RaisePropertiesChanged<T1, T2, T3, T4>(Expression<Func<T1>> expression1,
            Expression<Func<T2>> expression2, Expression<Func<T3>> expression3, Expression<Func<T4>> expression4)
        {
            RaisePropertyChanged(expression1);
            RaisePropertyChanged(expression2);
            RaisePropertyChanged(expression3);
            RaisePropertyChanged(expression4);
        }

        protected void RaisePropertiesChanged<T1, T2, T3, T4, T5>(Expression<Func<T1>> expression1,
            Expression<Func<T2>> expression2, Expression<Func<T3>> expression3, Expression<Func<T4>> expression4,
            Expression<Func<T5>> expression5)
        {
            RaisePropertyChanged(expression1);
            RaisePropertyChanged(expression2);
            RaisePropertyChanged(expression3);
            RaisePropertyChanged(expression4);
            RaisePropertyChanged(expression5);
        }

        #endregion

        #region property bag

        private Dictionary<string, object> _propertyBag;
        private Dictionary<string, object> PropertyBag =>
            _propertyBag ?? (_propertyBag = new Dictionary<string, object>());
#if DEBUG
        internal Dictionary<string, object> PropertyBagForTests => PropertyBag;
#endif
        private T GetPropertyCore<T>(string propertyName)
        {
            object val;
            if (PropertyBag.TryGetValue(propertyName, out val))
                return (T) val;
            return default;
        }

        private bool SetPropertyCore<T>(string propertyName, T value, Action changedCallback)
        {
            T oldValue;
            var res = SetPropertyCore(propertyName, value, out oldValue);
            if (res) changedCallback?.Invoke();
            return res;
        }

        private bool SetPropertyCore<T>(string propertyName, T value, Action<T> changedCallback)
        {
            T oldValue;
            var res = SetPropertyCore(propertyName, value, out oldValue);
            if (res) changedCallback?.Invoke(oldValue);
            return res;
        }

        protected virtual bool SetPropertyCore<T>(string propertyName, T value, out T oldValue)
        {
            VerifyAccess();
            oldValue = default;
            object val;
            if (PropertyBag.TryGetValue(propertyName, out val))
                oldValue = (T) val;
            if (CompareValues(oldValue, value))
                return false;
            lock (PropertyBag)
            {
                PropertyBag[propertyName] = value;
            }

            RaisePropertyChanged(propertyName);
            return true;
        }

        protected virtual void VerifyAccess()
        {
        }

        private static bool CompareValues<T>(T storage, T value)
        {
            return Equals(storage, value);
        }

        #endregion
    }
}