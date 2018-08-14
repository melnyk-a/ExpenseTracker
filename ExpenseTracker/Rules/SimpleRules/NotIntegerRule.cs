using ExpenseTracker.ValidationRules;

namespace ExpenseTracker.Rules.SimpleRules
{
    internal sealed class NotIntegerRule : SimpleRule
    {
        public override void Check(string value)
        {
            if (!int.TryParse(value, out int number))
            {
                throw new ValidationErrorExeption("Specified value cannot be represented as integer.");
            }
        }
    }
}