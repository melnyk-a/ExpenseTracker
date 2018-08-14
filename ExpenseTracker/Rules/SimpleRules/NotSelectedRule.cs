using ExpenseTracker.ValidationRules;

namespace ExpenseTracker.Rules.SimpleRules
{
    internal sealed class NotSelectedRule : SimpleRule
    {
        public override void Check(string value)
        {
            if (value == null)
            {
                throw new ValidationErrorExeption("must be selected.");
            }
        }
    }
}