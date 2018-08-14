using System;

namespace ExpenseTracker.Models.BasicIdentities
{
    internal sealed class IdentityPropertyChangedEventArgs : EventArgs
    {
        private readonly BasicIdentity identity;
        private readonly string propertyName;

        public IdentityPropertyChangedEventArgs(BasicIdentity identity, 
                                                string propertyName
        )
        {
            this.propertyName = propertyName;
            this.identity = identity;
        }
        public BasicIdentity Identity => identity;

        public string PropertyName => propertyName;
    }
}