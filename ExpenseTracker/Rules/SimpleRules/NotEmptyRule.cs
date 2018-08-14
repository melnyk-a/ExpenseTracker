using ExpenseTracker.ValidationRules;

namespace ExpenseTracker.Rules.SimpleRules
{
    internal sealed class NotEmptyRule : SimpleRule
    {
        public override void Check(string value)
        {
            if (value.Length == 0)
            {
                throw new ValidationErrorExeption("cannot be empty.");
            }
        }
    }
}