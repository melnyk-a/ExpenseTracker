using ExpenseTracker.Models.Databases;

namespace ExpenseTracker.Rules.DatabaseRules
{
    internal abstract class DatabaseRule<T> : Rule 
    {
        private readonly IDatabaseProvider<T> database;

        public DatabaseRule(IDatabaseProvider<T> database)
        {
            this.database = database;
        }

        public abstract void Check(string value, string name);
    }
}