using ExpenseTracker.Models.Databases;
using ExpenseTracker.ValidationRules;

namespace ExpenseTracker.Rules.SimpleRules
{
    internal sealed class NotExistRule<T> : SimpleRule
    {
        private readonly IDatabaseProvider<T> database;

        public NotExistRule(IDatabaseProvider<T> database)
        {
            this.database = database;
        }

        public override void Check(string value)
        {
            if (database.Contains(value))
            {
                throw new ValidationErrorExeption("with specified name already exists.");
            }
        }
    }
}