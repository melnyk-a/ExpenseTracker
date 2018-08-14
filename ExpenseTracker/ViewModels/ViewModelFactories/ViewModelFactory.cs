using ExpenseTracker.Models.BasicIdentities;
using ExpenseTracker.Models.DataBases;
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
        private readonly SummaryDataBase summaryDataBase;

        public ViewModelFactory(SummaryDataBase summaryDataBase)
        {
            this.summaryDataBase = summaryDataBase;

            accountRuleProvider = new RuleProvider(new RuleFactory<Account>(summaryDataBase.AccountProvider));
            expenseRuleProvider = new RuleProvider(new RuleFactory<Expense>(summaryDataBase.ExpenseProvider));
        }

        public ViewModel CreateAccountViewModel(Account account)
        {
            return new AccountViewModel(summaryDataBase.AccountProvider, accountRuleProvider, account);
        }

        public ViewModel CreateAccountsWindowViewModel()
        {
            return new AccountsWindowViewModel(summaryDataBase.AccountProvider, accountRuleProvider, this);
        }

        public ViewModel CreateBillListViewModel()
        {
            return new BillListViewModel(summaryDataBase.BillProvider, this, selectedExpense);
        }

        public ViewModel CreateBillViewModel(Bill bill)
        {
            return new BillViewModel(summaryDataBase.BillProvider, bill);
        }

        public ViewModel CreateBillsWindowViewModel()
        {
            return new BillsWindowViewModel(summaryDataBase, accountRuleProvider, this);
        }

        public ViewModel CreateCategoryStatisticListViewModel()
        {
            return new CategoryStatisticListViewModel(summaryDataBase.ExpenseProvider, this, selectedExpense);
        }

        public ViewModel CreateCategoryStatisticViewModel(Expense expense)
        {
            return new CatagoryStatisticViewModel(summaryDataBase.BillProvider, expense);
        }

        public ViewModel CreateExpenseViewModel(Expense expense)
        {
            return new ExpenseViewModel(summaryDataBase.ExpenseProvider, expenseRuleProvider, expense);
        }

        public ViewModel CreateExpensesWindowViewModel()
        {
            return new ExpensesWindowViewModel(summaryDataBase.ExpenseProvider, expenseRuleProvider, this);
        }
    }
}