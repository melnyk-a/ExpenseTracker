using ExpenseTracker.Commands;
using ExpenseTracker.ViewModels.ViewModelFactories;
using System.Windows.Input;

namespace ExpenseTracker.ViewModels
{
    internal sealed class MainWindowViewModel : NotifyPropertyChangedViewModel
    {
        private readonly ViewModel accountsWindowViewModel;
        private readonly ViewModel billsWindowViewModel;
        private readonly ViewModel expensesWindowViewModel;
        private readonly ICommand showAccountsCommand;
        private readonly ICommand showBillsCommand;
        private readonly ICommand showExpensesCommand;

        private object current;

        public MainWindowViewModel(IViewModelFactory factory)
        {
            accountsWindowViewModel = factory.CreateAccountsWindowViewModel();
            expensesWindowViewModel = factory.CreateExpensesWindowViewModel();
            billsWindowViewModel = factory.CreateBillsWindowViewModel();

            showAccountsCommand = new DelegateCommand(() => Current = accountsWindowViewModel);
            showExpensesCommand = new DelegateCommand(() => Current = expensesWindowViewModel);
            showBillsCommand = new DelegateCommand(() => Current = billsWindowViewModel);

            current = billsWindowViewModel;
        }

        public object Current
        {
            get => current;
            set => SetProperty(ref current, value);
        }

        public ICommand ShowAccountsCommand => showAccountsCommand;

        public ICommand ShowBillsCommand => showBillsCommand;

        public ICommand ShowExpensesCommand => showExpensesCommand;
    }
}