using ExpenseTracker.Attributes;
using ExpenseTracker.Commands;
using ExpenseTracker.Models.BasicIdentities;
using ExpenseTracker.Models.DataBases;
using ExpenseTracker.Rules;
using ExpenseTracker.ViewModels.ViewModelFactories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ExpenseTracker.ViewModels.BillViewModels
{
    internal sealed class BillsWindowViewModel : NotifyDataErrorViewModel<Account>
    {
        private readonly ObservableCollection<Account> accountIcons = new ObservableCollection<Account>();
        private readonly Command addBillCommand;
        private readonly ViewModel billListViewModel;
        private readonly IDataBaseProvider<Bill> billProvider;
        private readonly ViewModel categoryStatisticListViewModel;
        private readonly ObservableCollection<Expense> expenseIcons = new ObservableCollection<Expense>();
        private readonly IViewModelFactory viewModelFactory;

        private string description = string.Empty;
        private Account selectedAccount = null;
        private string selectedDate = DateTime.Now.ToShortDateString();
        private Expense selectedExpense = null;
        private string sum = string.Empty;
       
        public BillsWindowViewModel(SummaryDataBase dataBase,
                                    IRuleProvider ruleProvider,
                                    IViewModelFactory viewModelFactory) :
            base(ruleProvider)
        {
            this.viewModelFactory = viewModelFactory;
            billProvider = dataBase.BillProvider;
            billListViewModel = viewModelFactory.CreateBillListViewModel();
            categoryStatisticListViewModel = viewModelFactory.CreateCategoryStatisticListViewModel();

            addBillCommand = new DelegateCommand(AddBill, () => CanAdd);

            foreach (Account account in dataBase.AccountProvider.Items)
            {
                accountIcons.Add(account);
            }

            foreach (Expense expense in dataBase.ExpenseProvider.Items)
            {
                expenseIcons.Add(expense);
            }

            dataBase.AccountProvider.DataBaseChanged += (sender, e) =>
            {
                if (e.ChangedAction == ChangedAction.Add)
                {
                    accountIcons.Add(e.DataItem);
                }
                else if (e.ChangedAction == ChangedAction.Remove)
                {
                    accountIcons.Remove(e.DataItem);
                    if (accountIcons.Count == 0)
                    {
                        SelectedAccount = null;
                    }
                }
                else
                {
                    if (HasErrors)
                    {
                        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Sum)));
                    }
                }
            };

            dataBase.ExpenseProvider.DataBaseChanged += (sender, e) =>
            {
                if (e.ChangedAction == ChangedAction.Add)
                {
                    expenseIcons.Add(e.DataItem);
                }
                else if (e.ChangedAction == ChangedAction.Remove)
                {
                    expenseIcons.Remove(e.DataItem);
                    if (expenseIcons.Count == 0)
                    {
                        SelectedExpense = null;
                    }
                }
            };
        }

        public IEnumerable<Account> AccountIcons => accountIcons;

        [RaiseCanExecuteDependsUpon(nameof(CanAdd))]
        public Command AddBillCommand => addBillCommand;

        public ViewModel BillListViewModel => billListViewModel;

        [DependsUponProperty(nameof(SelectedAccount))]
        [DependsUponProperty(nameof(SelectedExpense))]
        [DependsUponProperty(nameof(Sum))]
        [DependsUponProperty(nameof(Description))]
        [DependsUponProperty(nameof(SelectedDate))]
        [DependsUponProperty(nameof(HasErrors))]
        public bool CanAdd => !HasErrors;

        public ViewModel CategoryStatisticListViewModel => categoryStatisticListViewModel;

        [ValidateRule("NotEmptyRule", Name = "Bill description")]
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public IEnumerable<Expense> ExpenseIcons => expenseIcons;

        [ValidateRule("NotSelectedRule", Name = "Account icon")]
        public Account SelectedAccount
        {
            get => selectedAccount;
            set
            {
                if (selectedAccount == null || value == null)
                {
                    selectedAccount = value;
                    if (HasErrors)
                    {
                        OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedAccount)));
                    }
                }
                else
                {
                    SetProperty(ref selectedAccount, value);
                }
            }
        }

        public string SelectedAccountName
        {
            get
            {
                string name = null;
                if (selectedAccount != null)
                {
                    name = selectedAccount.Name;
                }
                return name;
            }
        }

        [ValidateRule("NotSelectedRule", Name = "Bill date")]
        public string SelectedDate
        {
            get => selectedDate;
            set
            {
                SetProperty(ref selectedDate, value);
            }
        }

        [ValidateRule("NotSelectedRule", Name = "Expense icon")]
        public Expense SelectedExpense
        {
            get => selectedExpense;
            set
            {
                if (selectedExpense == null || value == null)
                {
                    selectedExpense = value;
                    if (HasErrors)
                    {
                        OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedExpense)));
                    }
                }
                else
                {
                    SetProperty(ref selectedExpense, value);
                }
            }
        }

        [DependsUponProperty(nameof(SelectedAccount))]
        [ValidateRule("NotIntegerRule")]
        [ValidateRule("NotNegativeRule", Name = "Bill sum")]
        [ValidateRule("NotExceedSelectedItemValue", DependentDataBaseItemName = nameof(SelectedAccountName))]
        public string Sum
        {
            get => sum;
            set => SetProperty(ref sum, value);
        }

        void AddBill()
        {
            Validate();
            if (!HasErrors)
            {
                billProvider.AddItem(new Bill(selectedExpense.Name)
                {
                    Value = Convert.ToInt32(sum),
                    Date = Convert.ToDateTime(selectedDate),
                    ImagePath = selectedExpense.ImagePath,
                    AccountName = SelectedAccount.Name,
                    ExpenseCategoryName = SelectedExpense.Name
                });
            }
            else
            {
                addBillCommand.RaiseCanExecuteChanged();
            }

        }
    }
}