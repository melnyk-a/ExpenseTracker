using ExpenseTracker.Models.DataBases;
using ExpenseTracker.ValidationRules;

namespace ExpenseTracker.Rules.SimpleRules
{
    internal sealed class NotExistRule<T> : SimpleRule
    {
        private readonly IDataBaseProvider<T> dataBase;

        public NotExistRule(IDataBaseProvider<T> dataBase)
        {
            this.dataBase = dataBase;
        }

        public override void Check(string value)
        {
            if (dataBase.Contains(value))
            {
                throw new ValidationErrorExeption("with specified name already exists.");
            }
        }
    }
}