using ExpenseTracker.Models.BasicIdentities;
using System;

namespace ExpenseTracker.Models.SelectedCategories
{
    internal interface ISelectedExpenseProvider
    {
        Expense SelectedExpense { get; }

        event EventHandler SelectedExpenseChanged;
    }
}