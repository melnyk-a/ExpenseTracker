using ExpenseTracker.Models.BasicIdentities;
using ExpenseTracker.Models.DataBases;
using ExpenseTracker.Rules.DaraBaseRules;
using ExpenseTracker.Rules.SimpleRules;

namespace ExpenseTracker.Rules.RuleFactories
{
    internal sealed class RuleFactory<T> : 
        IRuleFactory 
        where T : BasicIdentity
    {
        private readonly IDataBaseProvider<T> dataBaseProvider;

        public RuleFactory(IDataBaseProvider<T> dataBaseProvider)
        {
            this.dataBaseProvider = dataBaseProvider;
        }

        public Rule CreateNotEmptyRule() => new NotEmptyRule();

        public Rule CreateNotExceedSelectedItemValue() => new NotExceedSelectedItemValue<T>(dataBaseProvider);

        public Rule CreateNotExistRule() => new NotExistRule<T>(dataBaseProvider);

        public Rule CreateNotIntegerRule() => new NotIntegerRule();

        public Rule CreateNotNegativeAffterAddRule() => new NotNegativeAffterAddRule<T>(dataBaseProvider);

        public Rule CreateNotNegativeRule() => new NotNagativeRule();

        public Rule CreateNotSelectedRule() => new NotSelectedRule();
    }
}