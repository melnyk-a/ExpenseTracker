namespace ExpenseTracker.Rules
{
    internal interface IRuleProvider
    {
        Rule ProvideRule(string ruleName);
    }
}