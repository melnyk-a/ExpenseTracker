namespace ExpenseTracker.Rules.SimpleRules
{
    internal abstract class SimpleRule : Rule
    {
        public abstract void Check(string value);
    }
}