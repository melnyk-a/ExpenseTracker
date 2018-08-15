using ExpenseTracker.Models.BasicIdentities;
using ExpenseTracker.Models.Databases;
using ExpenseTracker.ValidationRules;
using System;

namespace ExpenseTracker.Rules.DataBaseRules
{
    internal sealed class NotNegativeAfterAddRule<T> : 
        DatabaseRule<T> 
        where T : BasicIdentity
    {
        private readonly IDatabaseProvider<T> accountProvider;

        public NotNegativeAfterAddRule(IDatabaseProvider<T> accountProvider) :
            base(accountProvider)
        {
            this.accountProvider = accountProvider;
        }

        public override void Check(string value, string name)
        {
            T finded = accountProvider.Find(name);
            if (finded != null)
            {
                int result = finded.Value + Convert.ToInt32(value);
                if (result < 0)
                {
                    throw new ValidationErrorExeption("Adding specified value results in negative funds");
                }
            }
        }
    }
}