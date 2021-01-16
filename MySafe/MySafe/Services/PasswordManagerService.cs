﻿using MySafe.Services.Abstractions;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;

namespace MySafe.Services
{
    // TODO: Убрать из моделей, решить вопрос с RaiseNotify и большим количеством свойств
    public class PasswordManagerService : BindableBase, IPasswordManagerService, ITransientService
    {
        public ObservableCollection<string> PasswordCollection { get; }
        public string Password => string.Join("", PasswordCollection.Reverse());
        public int PasswordMaxLength => PasswordCollection.Count;
        public int PasswordLength => PasswordCollection.Count(x => !string.IsNullOrEmpty(x));


        public PasswordManagerService()
        {
            PasswordCollection = new ObservableCollection<string>();
            SetPasswordLength(5);
        }

        public void SetPasswordLength(int length)
        {
            PasswordCollection.Clear();

            for (var i = 0; i < length; i++)
            {
                PasswordCollection.Add(string.Empty);
            }
        }

        public bool TryAdd(string value)
        {
            if (PasswordLength == PasswordMaxLength)
                return false;

            var lastIndex = PasswordCollection.IndexOf(string.Empty);
            PasswordCollection[lastIndex] = value;

            return true;
        }

        public void RemoveLast()
        {
            var @string = PasswordCollection.LastOrDefault(x => !string.IsNullOrEmpty(x));

            if (@string != null)
            {
                var lastIndex = PasswordCollection.ToList().LastIndexOf(@string);
                PasswordCollection[lastIndex] = string.Empty;
            }
        }

        public void Clear()
        {
            for (var i = 0; i < PasswordMaxLength; i++)
            {
                PasswordCollection[i] = string.Empty;
            }
        }
    }
}