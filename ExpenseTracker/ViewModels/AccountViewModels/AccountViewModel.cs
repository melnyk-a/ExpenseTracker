using ExpenseTracker.Attributes;
using ExpenseTracker.Commands;
using ExpenseTracker.Models.BasicIdentities;
using ExpenseTracker.Models.Databases;
using ExpenseTracker.Rules;
using System;
using System.Windows.Input;

namespace ExpenseTracker.ViewModels.AccountViewModels
{
    internal sealed class AccountViewModel : NotifyDataErrorViewModel<Account>
    {
        private readonly Account account;
        private readonly IDatabaseProvider<Account> accountProvider;
        private readonly Command addFundsCommand;
        private readonly Command deleteAccountCommand;

        private string addedFund = string.Empty;
        private int funds;

        public AccountViewModel(IDatabaseProvider<Account> accountProvider, 
                                IRuleProvider ruleProvider, 
                                Account account) :
            base(ruleProvider)
        {
            this.accountProvider = accountProvider;
            this.account = account;
            funds = account.Value;

            addFundsCommand = new DelegateCommand(Add, () => CanAdd);
            deleteAccountCommand = new DelegateCommand(Delete);

            accountProvider.DatabaseChanged += (sender, e) =>
            {
                if (e.ChangedAction == ChangedAction.Update)
                {
                    if (this.account.Name.Equals(e.DataItem.Name))
                    {
                        Funds = e.DataItem.Value;
                    }
                }
            };
        }

        [ValidateRule("NotIntegerRule")]
        [ValidateRule("NotNegativeAffterAddRule", DependentDatabaseItemName = nameof(Name))]
        public string AddedFund
        {
            get => addedFund;
            set => SetProperty(ref addedFund, value);
        }

        [RaiseCanExecuteDependsUpon(nameof(CanAdd))]
        public ICommand AddFundsCommand => addFundsCommand;

        [DependsUponProperty(nameof(AddedFund))]
        [DependsUponProperty(nameof(HasErrors))]
        public bool CanAdd => !HasErrors;

        public ICommand DeleteAccountCommand => deleteAccountCommand;

        public int Funds
        {
            get => funds;
            private set => SetProperty(ref funds, value);
        }

        public string IconSourse => account.ImagePath;

        public string Name => account.Name;

        public void Add()
        {
            Validate();
            if (CanAdd)
            {
                int value = account.Value + Convert.ToInt32(addedFund); ;
                accountProvider.Find(account.Name).Value = value;
            }
            else
            {
                addFundsCommand.RaiseCanExecuteChanged();
            }
        }

        public void Delete()
        {
            accountProvider.DeleteItem(new Account(Name));
        }
    }
}