using ExpenseTracker.Attributes;
using ExpenseTracker.Commands;
using ExpenseTracker.Models;
using ExpenseTracker.Models.BasicIdentities;
using ExpenseTracker.Models.Databases;
using ExpenseTracker.Rules;
using ExpenseTracker.ViewModels.ViewModelFactories;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ExpenseTracker.ViewModels.ExpenseViewModels
{
    internal sealed class ExpensesWindowViewModel : NotifyDataErrorViewModel<Expense>
    {
        private readonly Command addExpanseCommand;
        private readonly ObservableCollection<ViewModel> expenseList = new ObservableCollection<ViewModel>();
        private readonly IDatabaseProvider<Expense> expenseProvider;
        private readonly ObservableCollection<string> icons = new ObservableCollection<string>();
        private readonly IViewModelFactory viewModelFactory;

        private string name = string.Empty;
        private object selectedIcon;

        public ExpensesWindowViewModel(IDatabaseProvider<Expense> expenseProvider, 
                                       IRuleProvider ruleProvider, 
                                       IViewModelFactory viewModelFactory) :
            base(ruleProvider)
        {
            this.expenseProvider = expenseProvider;
            this.viewModelFactory = viewModelFactory;

            LoadFromExpenseProvider();
            LoadExpenseIcons();

            addExpanseCommand = new DelegateCommand(AddExpense, () => CanAdd);

            expenseProvider.DatabaseChanged += (sender, e) =>
            {
                if (e.ChangedAction == ChangedAction.Add)
                {
                    var accountViewModel = viewModelFactory.CreateExpenseViewModel(e.DataItem);
                    expenseList.Add(accountViewModel);
                }
                else if (e.ChangedAction == ChangedAction.Remove)
                {
                    foreach (ExpenseViewModel expense in expenseList)
                    {
                        if (expense.Name.Equals(e.DataItem.Name))
                        {
                            expenseList.Remove(expense);
                            if (HasErrors)
                            {
                                Validate();
                            }
                            break;
                        }
                    }
                }
            };
        }

        [RaiseCanExecuteDependsUpon(nameof(CanAdd))]
        public Command AddExpanseCommand => addExpanseCommand;

        [DependsUponProperty(nameof(Name))]
        [DependsUponProperty(nameof(SelectedIcon))]
        [DependsUponProperty(nameof(HasErrors))]
        public bool CanAdd => !HasErrors;

        public IEnumerable<ViewModel> Expenses => expenseList;

        public IEnumerable<string> Icons => icons;

        [ValidateRule("NotEmptyRule", Name = "Expanse name")]
        [ValidateRule("NotExistRule", Name = "Expanse name")]
        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
            }
        }

        [ValidateRule("NotSelectedRule", Name = "Expanse icon")]
        public object SelectedIcon
        {
            get => selectedIcon;
            set
            {
                SetProperty(ref selectedIcon, value);
            }
        }

        void AddExpense()
        {
            Validate();
            if (!HasErrors)
            {
                expenseProvider.AddItem(new Expense(Name) { ImagePath = (string)selectedIcon });
            }
            else
            {
                addExpanseCommand.RaiseCanExecuteChanged();
            }
        }

        private void LoadExpenseIcons()
        {
            foreach (string path in CategoryImages.Paths)
            {
                icons.Add(path);
            }
        }

        private void LoadFromExpenseProvider()
        {
            foreach (Expense expense in expenseProvider.Items)
            {
                var accountViewModel = viewModelFactory.CreateExpenseViewModel(expense);
                expenseList.Add(accountViewModel);
            }
        }
    }
}