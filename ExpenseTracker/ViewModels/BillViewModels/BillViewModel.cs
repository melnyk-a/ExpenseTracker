using ExpenseTracker.Commands;
using ExpenseTracker.Models.BasicIdentities;
using ExpenseTracker.Models.Databases;
using System;
using System.Windows.Input;

namespace ExpenseTracker.ViewModels.BillViewModels
{
    internal sealed class BillViewModel : ViewModel
    {
        private readonly Bill bill;
        private readonly IDatabaseProvider<Bill> billProvider;
        private readonly ICommand deleteBillCommand;

        public BillViewModel(IDatabaseProvider<Bill> billProvider, Bill bill)
        {
            this.bill = bill;
            this.billProvider = billProvider;

            deleteBillCommand = new DelegateCommand(DeleteBill);
        }

        public DateTime Date => bill.Date;

        public ICommand DeleteBillCommand => deleteBillCommand;

        public string IconSourse => bill.ImagePath;

        public string Name => bill.Name;

        public int Sum => bill.Value;

        private void DeleteBill() => billProvider.DeleteItem(bill);
    }
}