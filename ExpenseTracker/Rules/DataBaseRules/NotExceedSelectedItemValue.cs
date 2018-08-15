using ExpenseTracker.Models.BasicIdentities;
using ExpenseTracker.Models.Databases;
using ExpenseTracker.ValidationRules;
using System;

namespace ExpenseTracker.Rules.DataBaseRules
{
    internal sealed class NotExceedSelectedItemValue<T> : 
        DatabaseRule<T> 
        where T : BasicIdentity
    {
        private readonly IDatabaseProvider<T> accountProvider;

        public NotExceedSelectedItemValue(IDatabaseProvider<T> accountProvider) :
            base(accountProvider)
        {
            this.accountProvider = accountProvider;
        }

        public override void Check(string value, string name)
        {
            T found = accountProvider.Find(name);
            if (found != null)
            {
                int result = (found).Value - Convert.ToInt32(value);
                if (result < 0)
                {
                    throw new ValidationErrorExeption("Specified sum exceeds selected account funds.");
                }
            }
        }
    }
}