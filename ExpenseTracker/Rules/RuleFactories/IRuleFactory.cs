namespace ExpenseTracker.Rules.RuleFactories
{
    internal interface IRuleFactory
    {
        Rule CreateNotEmptyRule();
        Rule CreateNotExceedSelectedItemValue();
        Rule CreateNotExistRule();
        Rule CreateNotIntegerRule();
        Rule CreateNotNegativeAffterAddRule();
        Rule CreateNotNegativeRule();
        Rule CreateNotSelectedRule();
    }
}