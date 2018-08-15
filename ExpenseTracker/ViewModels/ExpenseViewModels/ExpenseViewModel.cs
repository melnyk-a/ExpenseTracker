using ExpenseTracker.Attributes;
using ExpenseTracker.Commands;
using ExpenseTracker.Models.BasicIdentities;
using ExpenseTracker.Models.Databases;
using ExpenseTracker.Rules;
using System;
using System.Windows.Input;

namespace ExpenseTracker.ViewModels.ExpenseViewModels
{
    internal sealed class ExpenseViewModel : NotifyDataErrorViewModel<Expense>
    {
        private readonly Command deleteExpanseCommand;
        private readonly Expense expense;
        private readonly IDatabaseProvider<Expense> expenseProvider;
        private readonly Command setLimitCommand;

        private int limit;
        private string setlimit = string.Empty;

        public ExpenseViewModel(IDatabaseProvider<Expense> expenseProvider, 
                                IRuleProvider ruleProvider, 
                                Expense expense) :
            base(ruleProvider)
        {
            this.expense = expense;
            this.expenseProvider = expenseProvider;
            limit = expense.Value;

            setLimitCommand = new DelegateCommand(SetLimitForExpense, () => CanSet);
            deleteExpanseCommand = new DelegateCommand(Delete);

            expenseProvider.DatabaseChanged += (sender, e) =>
            {
                if (e.ChangedAction == ChangedAction.Update)
                {
                    if (this.expense.Name.Equals(e.DataItem.Name))
                    {
                        Limit = e.DataItem.Value;
                    }
                }
            };
        }

        [DependsUponProperty(nameof(SetLimit))]
        [DependsUponProperty(nameof(HasErrors))]
        public bool CanSet => !HasErrors;

        public ICommand DeleteExpanseCommand => deleteExpanseCommand;

        public string IconSourse => expense.ImagePath;

        public int Limit
        {
            get => limit;
            private set
            {
                SetProperty(ref limit, value);
            }
        }

        public string Name => expense.Name;

        [ValidateRule("NotIntegerRule")]
        [ValidateRule("NotNegativeAffterAddRule", DependentDatabaseItemName = nameof(Name))]
        public string SetLimit
        {
            get => setlimit;
            set
            {
                SetProperty(ref setlimit, value);
            }
        }

        [RaiseCanExecuteDependsUpon(nameof(CanSet))]
        public ICommand SetLimitCommand => setLimitCommand;

        public void Delete()
        {
            expenseProvider.DeleteItem(new Expense(Name));
        }

        public void SetLimitForExpense()
        {
            Validate();
            if (CanSet)
            {
                expenseProvider.Find(expense.Name).Value = Convert.ToInt32(setlimit);
            }
            else
            {
                setLimitCommand.RaiseCanExecuteChanged();
            }
        }
    }
}