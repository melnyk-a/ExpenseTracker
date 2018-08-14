using ExpenseTracker.ValidationRules;
using System;

namespace ExpenseTracker.Rules.SimpleRules
{
    internal sealed class NotNagativeRule : SimpleRule
    {
        public override void Check(string value)
        {
            if (Convert.ToInt32(value) < 0)
            {
                throw new ValidationErrorExeption("cannot be negative.");
            }
        }
    }
}