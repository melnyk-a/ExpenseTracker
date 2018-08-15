using ExpenseTracker.Models.Databases;

namespace ExpenseTracker.Rules.DataBaseRules
{
    internal abstract class DatabaseRule<T> : Rule 
    {
        private readonly IDatabaseProvider<T> database;

        public DatabaseRule(IDatabaseProvider<T> dataBase)
        {
            this.database = dataBase;
        }

        public abstract void Check(string value, string name);
    }
}