using ExpenseTracker.Attributes;
using ExpenseTracker.Commands;
using ExpenseTracker.Models;
using ExpenseTracker.Models.BasicIdentities;
using ExpenseTracker.Models.DataBases;
using ExpenseTracker.Rules;
using ExpenseTracker.ViewModels.ViewModelFactories;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ExpenseTracker.ViewModels.AccountViewModels
{
    internal sealed class AccountsWindowViewModel : NotifyDataErrorViewModel<Account>
    {
        private readonly IDataBaseProvider<Account> accountProvider;
        private readonly ObservableCollection<ViewModel> accountList = new ObservableCollection<ViewModel>();
        private readonly Command addAccountCommand;
        private readonly ObservableCollection<string> icons = new ObservableCollection<string>();
        private readonly IViewModelFactory viewModelFactory;

        private string name = string.Empty;
        private object selectedIcon;

        public AccountsWindowViewModel(IDataBaseProvider<Account> accountProvider, 
                                       IRuleProvider ruleProvider, 
                                       IViewModelFactory viewModelFactory) :
            base(ruleProvider)
        {
            this.accountProvider = accountProvider;
            this.viewModelFactory = viewModelFactory;

            addAccountCommand = new DelegateCommand(AddAccount, () => CanAdd);

            LoadFromAccountProvider();
            LoadAccountIcons();

            accountProvider.DataBaseChanged += (sender, e) =>
            {
                if (e.ChangedAction == ChangedAction.Add)
                {
                    var accountViewModel = viewModelFactory.CreateAccountViewModel(e.DataItem);
                    accountList.Add(accountViewModel);
                }
                else if (e.ChangedAction == ChangedAction.Remove)
                {
                    foreach (AccountViewModel account in accountList)
                    {
                        if (account.Name.Equals(e.DataItem.Name))
                        {
                            accountList.Remove(account);
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

        public IEnumerable<ViewModel> Accounts => accountList;

        [RaiseCanExecuteDependsUpon(nameof(CanAdd))]
        public Command AddAccountCommand => addAccountCommand;

        [DependsUponProperty(nameof(Name))]
        [DependsUponProperty(nameof(SelectedIcon))]
        [DependsUponProperty(nameof(HasErrors))]
        public bool CanAdd => !HasErrors;

        public IEnumerable<string> Icons => icons;

        [ValidateRule("NotEmptyRule", Name = "Account name")]
        [ValidateRule("NotExistRule", Name = "Account name")]
        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
            }
        }

        [ValidateRule("NotSelectedRule", Name = "Account icon")]
        public object SelectedIcon
        {
            get => selectedIcon;
            set
            {
                SetProperty(ref selectedIcon, value);
            }
        }

        void AddAccount()
        {
            Validate();
            if (!HasErrors)
            {
               accountProvider.AddItem(new Account(Name) { ImagePath = (string)selectedIcon });
            }
            else
            {
                addAccountCommand.RaiseCanExecuteChanged();
            }
        }

        private void LoadAccountIcons()
        {
            foreach (string path in AccountImages.Pathes)
            {
                icons.Add(path);
            }
        }

        private void LoadFromAccountProvider()
        {
            foreach (Account account in accountProvider.Items)
            {
                var accountViewModel = viewModelFactory.CreateAccountViewModel(account);
                accountList.Add(accountViewModel);
            }
        }
    }
}