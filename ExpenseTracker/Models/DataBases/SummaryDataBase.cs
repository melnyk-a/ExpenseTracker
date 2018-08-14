using ExpenseTracker.Models.BasicIdentities;
using ExpenseTracker.Models.XmlDocuments;
using System;

namespace ExpenseTracker.Models.DataBases
{
    internal sealed class SummaryDataBase
    {
        private readonly IDataBaseProvider<Account> accountProvider = new DataBaseProvider<Account>();
        private readonly IDataBaseProvider<Bill> billProvider = new DataBaseProvider<Bill>();
        private readonly IDataBaseProvider<Expense> expenseProvider = new DataBaseProvider<Expense>();

        public SummaryDataBase()
        {
            Load(accountProvider);
            Load(expenseProvider);
            Load(billProvider);

            accountProvider.DataBaseChanged += (sender, e) =>
            {
                if (e.ChangedAction == ChangedAction.Remove)
                {
                    DeleteBillByAccount(e.DataItem.Name);
                }
            };

            expenseProvider.DataBaseChanged += (sender, e) =>
            {
                if (e.ChangedAction == ChangedAction.Remove)
                {
                    DeleteBillByCategory(e.DataItem.Name);
                }
            };

            billProvider.DataBaseChanged += (sender, e) =>
            {
                if(e.ChangedAction==ChangedAction.Add)
                {
                    var account = accountProvider.Find(e.DataItem.AccountName);
                    account.Value = account.Value - Convert.ToInt32(e.DataItem.Value);
                }
                if (e.ChangedAction == Models.DataBases.ChangedAction.Remove)
                {
                    ReturnFunds(e.DataItem.AccountName, e.DataItem.Value);
                }
            };
        }

        public IDataBaseProvider<Account> AccountProvider => accountProvider;

        public IDataBaseProvider<Bill> BillProvider => billProvider;

        public IDataBaseProvider<Expense> ExpenseProvider => expenseProvider;

        private void DeleteBillByAccount(string billName)
        {
            for (int i = billProvider.Items.Count-1; i>=0; --i)
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

        private void Load<T>(IDataBaseProvider<T> dataBaseProvider)
        {
            XmlDataBaseDocument<T> dataBaseXml = new XmlDataBaseDocument<T>(dataBaseProvider);
            dataBaseXml.Load();
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