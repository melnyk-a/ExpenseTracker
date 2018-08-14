using ExpenseTracker.Models.BasicIdentities;
using ExpenseTracker.Models.DataBases;

namespace ExpenseTracker.Rules.DaraBaseRules
{
    internal abstract class DataBaseRule<T> : Rule 
    {
        private readonly IDataBaseProvider<T> dataBase;

        public DataBaseRule(IDataBaseProvider<T> dataBase)
        {
            this.dataBase = dataBase;
        }

        public abstract void Check(string value, string name);
    }
}