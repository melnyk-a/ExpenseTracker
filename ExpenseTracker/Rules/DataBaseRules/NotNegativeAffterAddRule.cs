using ExpenseTracker.Models.BasicIdentities;
using ExpenseTracker.Models.DataBases;
using ExpenseTracker.ValidationRules;
using System;

namespace ExpenseTracker.Rules.DaraBaseRules
{
    internal sealed class NotNegativeAffterAddRule<T> : 
        DataBaseRule<T> 
        where T : BasicIdentity
    {
        private readonly IDataBaseProvider<T> accountProvider;

        public NotNegativeAffterAddRule(IDataBaseProvider<T> accountProvider) :
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