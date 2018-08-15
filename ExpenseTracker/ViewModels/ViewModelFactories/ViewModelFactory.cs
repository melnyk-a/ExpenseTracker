using ExpenseTracker.Models.BasicIdentities;
using ExpenseTracker.Models.Databases;
using ExpenseTracker.Models.SelectedCategories;
using ExpenseTracker.Rules;
using ExpenseTracker.Rules.RuleFactories;
using ExpenseTracker.ViewModels.AccountViewModels;
using ExpenseTracker.ViewModels.BillViewModels;
using ExpenseTracker.ViewModels.ExpenseViewModels;

namespace ExpenseTracker.ViewModels.ViewModelFactories
{
    internal sealed class ViewModelFactory : IViewModelFactory
    {
        private readonly IRuleProvider accountRuleProvider;
        private readonly IRuleProvider expenseRuleProvider;
        private readonly SelectedExpenseManager selectedExpense = new SelectedExpenseManager();
        private readonly SummaryDatabase summaryDatabase;

        public ViewModelFactory(SummaryDatabase summaryDatabase)
        {
            this.summaryDatabase = summaryDatabase;

            accountRuleProvider = new RuleProvider(new RuleFactory<Account>(summaryDatabase.AccountProvider));
            expenseRuleProvider = new RuleProvider(new RuleFactory<Expense>(summaryDatabase.ExpenseProvider));
        }

        public ViewModel CreateAccountViewModel(Account account)
        {
            return new AccountViewModel(summaryDatabase.AccountProvider, accountRuleProvider, account);
        }

        public ViewModel CreateAccountsWindowViewModel()
        {
            return new AccountsWindowViewModel(summaryDatabase.AccountProvider, accountRuleProvider, this);
        }

        public ViewModel CreateBillListViewModel()
        {
            return new BillListViewModel(summaryDatabase.BillProvider, this, selectedExpense);
        }

        public ViewModel CreateBillViewModel(Bill bill)
        {
            return new BillViewModel(summaryDatabase.BillProvider, bill);
        }

        public ViewModel CreateBillsWindowViewModel()
        {
            return new BillsWindowViewModel(summaryDatabase, accountRuleProvider, this);
        }

        public ViewModel CreateCategoryStatisticListViewModel()
        {
            return new CategoryStatisticListViewModel(summaryDatabase.ExpenseProvider, this, selectedExpense);
        }

        public ViewModel CreateCategoryStatisticViewModel(Expense expense)
        {
            return new CatagoryStatisticViewModel(summaryDatabase.BillProvider, expense);
        }

        public ViewModel CreateExpenseViewModel(Expense expense)
        {
            return new ExpenseViewModel(summaryDatabase.ExpenseProvider, expenseRuleProvider, expense);
        }

        public ViewModel CreateExpensesWindowViewModel()
        {
            return new ExpensesWindowViewModel(summaryDatabase.ExpenseProvider, expenseRuleProvider, this);
        }
    }
}