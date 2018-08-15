using ExpenseTracker.Models.BasicIdentities;
using ExpenseTracker.Models.Databases;
using ExpenseTracker.Models.SelectedCategories;
using ExpenseTracker.ViewModels.ViewModelFactories;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ExpenseTracker.ViewModels.BillViewModels
{
    internal sealed class BillListViewModel : NotifyPropertyChangedViewModel
    {
        private readonly ObservableCollection<ViewModel> allBills = new ObservableCollection<ViewModel>();
        private readonly IDatabaseProvider<Bill> billProvider;
        private readonly ISelectedExpenseProvider selectedExpense;
        private readonly IViewModelFactory viewModelFactory;

        private ObservableCollection<ViewModel> selectedBills = new ObservableCollection<ViewModel>();

        public BillListViewModel(IDatabaseProvider<Bill> billProvider,
                                 IViewModelFactory viewModelFactory,
                                 ISelectedExpenseProvider selectedExpense
        )
        {
            this.billProvider = billProvider;
            this.selectedExpense = selectedExpense;
            this.viewModelFactory = viewModelFactory;

            LoadFromBillProvider();

            selectedExpense.SelectedExpenseChanged += (sender, e) =>
              {
                  OnPropertyChanged(new PropertyChangedEventArgs(nameof(Bills)));
              };

            billProvider.DatabaseChanged += (sender, e) =>
            {
                if (e.ChangedAction == ChangedAction.Add)
                {
                    allBills.Add(viewModelFactory.CreateBillViewModel(e.DataItem));
                }
                else if (e.ChangedAction == ChangedAction.Remove)
                {
                    foreach (BillViewModel bill in allBills)
                    {
                        if (bill.Name.Equals(e.DataItem.Name))
                        {
                            allBills.Remove(bill);
                            break;
                        }
                    }
                }
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Bills)));
            };
        }

        public IEnumerable<ViewModel> Bills
        {
            get
            {
                ObservableCollection<ViewModel> result = new ObservableCollection<ViewModel>();
                if (selectedExpense.SelectedExpense != null)
                {
                    foreach (ViewModel model in allBills)
                    {
                        if (((BillViewModel)model).Name == selectedExpense.SelectedExpense.Name)
                        {
                            result.Add(model);
                        }
                    }
                    selectedBills = result;
                }
                else
                {
                    selectedBills = allBills;
                }
                return selectedBills;
            }
        }

        private void LoadFromBillProvider()
        {
            foreach (Bill bill in billProvider.Items)
            {
                var billViewModel = viewModelFactory.CreateBillViewModel(bill);
                allBills.Add(billViewModel);
            }
        }
    }
}