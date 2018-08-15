using ExpenseTracker.Models.BasicIdentities;
using ExpenseTracker.Models.Databases;
using ExpenseTracker.Rules.DataBaseRules;
using ExpenseTracker.Rules.SimpleRules;

namespace ExpenseTracker.Rules.RuleFactories
{
    internal sealed class RuleFactory<T> : 
        IRuleFactory 
        where T : BasicIdentity
    {
        private readonly IDatabaseProvider<T> databaseProvider;

        public RuleFactory(IDatabaseProvider<T> databaseProvider)
        {
            this.databaseProvider = databaseProvider;
        }

        public Rule CreateNotEmptyRule() => new NotEmptyRule();

        public Rule CreateNotExceedSelectedItemValue() => new NotExceedSelectedItemValue<T>(databaseProvider);

        public Rule CreateNotExistRule() => new NotExistRule<T>(databaseProvider);

        public Rule CreateNotIntegerRule() => new NotIntegerRule();

        public Rule CreateNotNegativeAffterAddRule() => new NotNegativeAfterAddRule<T>(databaseProvider);

        public Rule CreateNotNegativeRule() => new NotNagativeRule();

        public Rule CreateNotSelectedRule() => new NotSelectedRule();
    }
}