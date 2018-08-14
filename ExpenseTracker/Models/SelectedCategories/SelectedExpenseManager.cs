using ExpenseTracker.Models.BasicIdentities;
using System;

namespace ExpenseTracker.Models.SelectedCategories
{
    internal sealed class SelectedExpenseManager : 
        ISelectedExpenseManager, 
        ISelectedExpenseProvider
    {
        private Expense selectedExpense;

        public Expense SelectedExpense
        {
            get => selectedExpense;
            set
            {
                if (selectedExpense != value)
                {
                    selectedExpense = value;
                    OnSelectedExpenseChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler SelectedExpenseChanged;

        public void OnSelectedExpenseChanged(EventArgs e)
        {
            SelectedExpenseChanged?.Invoke(this, e);
        }
    }
}