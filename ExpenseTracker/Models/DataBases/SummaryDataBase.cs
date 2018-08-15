using ExpenseTracker.Models.BasicIdentities;
using ExpenseTracker.Models.XmlDocuments;
using System;

namespace ExpenseTracker.Models.Databases
{
    internal sealed class SummaryDatabase
    {
        private readonly IDatabaseProvider<Account> accountProvider = new DatabaseProvider<Account>();
        private readonly IDatabaseProvider<Bill> billProvider = new DatabaseProvider<Bill>();
        private readonly IDatabaseProvider<Expense> expenseProvider = new DatabaseProvider<Expense>();

        public SummaryDatabase()
        {
            Load(accountProvider);
            Load(expenseProvider);
            Load(billProvider);

            accountProvider.DatabaseChanged += (sender, e) =>
            {
                if (e.ChangedAction == ChangedAction.Remove)
                {
                    DeleteBillByAccount(e.DataItem.Name);
                }
            };

            expenseProvider.DatabaseChanged += (sender, e) =>
            {
                if (e.ChangedAction == ChangedAction.Remove)
                {
                    DeleteBillByCategory(e.DataItem.Name);
                }
            };

            billProvider.DatabaseChanged += (sender, e) =>
            {
                if (e.ChangedAction == ChangedAction.Add)
                {
                    var account = accountProvider.Find(e.DataItem.AccountName);
                    account.Value = account.Value - Convert.ToInt32(e.DataItem.Value);
                }
                if (e.ChangedAction == Models.Databases.ChangedAction.Remove)
                {
                    ReturnFunds(e.DataItem.AccountName, e.DataItem.Value);
                }
            };
        }

        public IDatabaseProvider<Account> AccountProvider => accountProvider;

        public IDatabaseProvider<Bill> BillProvider => billProvider;

        public IDatabaseProvider<Expense> ExpenseProvider => expenseProvider;

        private void DeleteBillByAccount(string billName)
        {
            for (int i = billProvider.Items.Count - 1; i >= 0; --i)
            {
                if (billProvider.Items[i].AccountName == billName)
                {
                    billProvider.DeleteItem(billProvider.Items[i]);

                }
            }
        }

        private void DeleteBillByCategory(string billName)
        {
            for (int i = billProvider.Items.Count - 1; i >= 0; --i)
            {
                if (billProvider.Items[i].ExpenseCategoryName == billName)
                {
                    billProvider.DeleteItem(billProvider.Items[i]);
                }
            }
        }

        private void Load<T>(IDatabaseProvider<T> databaseProvider)
        {
            XmlDataBaseDocument<T> databaseXml = new XmlDataBaseDocument<T>(databaseProvider);
            databaseXml.Load();
        }

        private void ReturnFunds(string accountName, int value)
        {
            var finded = accountProvider.Find(accountName);
            if (finded != null)
            {
                finded.Value = finded.Value + value;
            }
        }
    }
}