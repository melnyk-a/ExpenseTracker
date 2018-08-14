using ExpenseTracker.Models.BasicIdentities;

namespace ExpenseTracker.ViewModels.ViewModelFactories
{
    internal interface IViewModelFactory
    {
        ViewModel CreateAccountViewModel(Account account);
        ViewModel CreateAccountsWindowViewModel();
        ViewModel CreateBillListViewModel();
        ViewModel CreateBillViewModel(Bill bill);
        ViewModel CreateBillsWindowViewModel();
        ViewModel CreateCategoryStatisticListViewModel();
        ViewModel CreateCategoryStatisticViewModel(Expense expense);
        ViewModel CreateExpenseViewModel(Expense expense);
        ViewModel CreateExpensesWindowViewModel();
    }
}