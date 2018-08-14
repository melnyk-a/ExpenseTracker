using ExpenseTracker.Models.BasicIdentities;

namespace ExpenseTracker.Models.SelectedCategories
{
    internal interface ISelectedExpenseManager
    {
        Expense SelectedExpense { get; set; }
    }
}