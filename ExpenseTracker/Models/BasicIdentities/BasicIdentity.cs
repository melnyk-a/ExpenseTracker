using System;
using System.Runtime.CompilerServices;

namespace ExpenseTracker.Models.BasicIdentities
{
    internal abstract class BasicIdentity
    {
        private readonly string name;

        public BasicIdentity(string name)
        {
            this.name = name;
        }

        public string Name => name;

        public abstract int Value { get; set; }

        public event EventHandler<IdentityPropertyChangedEventArgs> IdentityChanged;

        public void OnIdentityChanged(IdentityPropertyChangedEventArgs e)
        {
            IdentityChanged?.Invoke(this, e);
        }

        protected void SetProperty<T>(ref T oldValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!(oldValue?.Equals(newValue)) ?? newValue != null)
            {
                oldValue = newValue;
                OnIdentityChanged(new IdentityPropertyChangedEventArgs(this, propertyName));
            }
        }
    }
}