using ExpenseTracker.Attributes;
using ExpenseTracker.Models.BasicIdentities;
using ExpenseTracker.Models.Databases;
using ExpenseTracker.ViewModels.BillViewModels;

namespace ExpenseTracker.ViewModels.BillViewModels
{
    internal sealed class CatagoryStatisticViewModel : NotifyPropertyChangedViewModel
    {
        private readonly IDatabaseProvider<Bill> billProvider;
        private readonly Expense expense;

        private ExpenseLimit expenseLimit;
        private double limit;
        private int percentage;
        private double sumOfBills;

        public CatagoryStatisticViewModel(IDatabaseProvider<Bill> billProvider,
                                          Expense expense
        )
        {
            this.expense = expense;
            this.billProvider = billProvider;
            limit = expense.Value;

            CalculateSumOfBills();

            expense.IdentityChanged += (sender, e) =>
              {
                  Limit = e.Identity.Value;
              };

            billProvider.DatabaseChanged += (sender, e) =>
              {
                  if (e.ChangedAction == ChangedAction.Add || e.ChangedAction == ChangedAction.Remove)
                  {
                      CalculateSumOfBills();
                  }
              };
        }

        [DependsUponProperty(nameof(Limit))]
        [DependsUponProperty(nameof(SumOfBills))]
        public string Description => $"{SumOfBills} ({Percentage}%) of ${Limit}";

        public Expense Expense => expense;

        [DependsUponProperty(nameof(Percentage))]
        public ExpenseLimit ExpenseLimit
        {
            get
            {
                expenseLimit = percentage <= 75 ? ExpenseLimit.Low :
                    percentage > 75 && percentage <= 100 ? ExpenseLimit.Normal :
                    ExpenseLimit.Hight;

                return expenseLimit;
            }
        }
        public string IconSourse => expense.ImagePath;

        public double Limit
        {
            get => limit;
            set => SetProperty(ref limit, value);
        }

        public string Name => expense.Name;

        [DependsUponProperty(nameof(Limit))]
        [DependsUponProperty(nameof(SumOfBills))]
        public int Percentage
        {
            get
            {
                if (limit != 0)
                {
                    percentage = (int)(sumOfBills / limit * 100);
                }
                else
                {
                    percentage = (int)sumOfBills;
                }
                return percentage;
            }
        }


        public double SumOfBills
        {
            get => sumOfBills;
            set => SetProperty(ref sumOfBills, value);
        }

        private void CalculateSumOfBills()
        {
            int sum = 0;
            foreach (Bill bill in billProvider.Items)
            {
                if (bill.ExpenseCategoryName == Name)
                {
                    sum += bill.Value;
                }
            }
            SumOfBills = sum;
        }
    }
}