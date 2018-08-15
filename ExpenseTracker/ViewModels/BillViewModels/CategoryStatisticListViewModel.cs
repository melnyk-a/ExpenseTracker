using ExpenseTracker.Models.BasicIdentities;
using ExpenseTracker.Models.Databases;
using ExpenseTracker.Models.SelectedCategories;
using ExpenseTracker.ViewModels.ViewModelFactories;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ExpenseTracker.ViewModels.BillViewModels
{
    internal sealed class CategoryStatisticListViewModel : NotifyPropertyChangedViewModel
    {
        private readonly ObservableCollection<ViewModel> expenseCategoryStatistics = new ObservableCollection<ViewModel>();
        private readonly IDatabaseProvider<Expense> expenseProvider;
        private readonly ISelectedExpenseManager selectedManager;
        private readonly IViewModelFactory viewModelFactory;

        private CatagoryStatisticViewModel selectedCategory;

        public CategoryStatisticListViewModel(IDatabaseProvider<Expense> expenseProvider,
                                              IViewModelFactory viewModelFactory,
                                              ISelectedExpenseManager selectedManager
        )
        {
            this.expenseProvider = expenseProvider;
            this.viewModelFactory = viewModelFactory;
            this.selectedManager = selectedManager;

            LoadFromExpenseProvider();

            expenseProvider.DatabaseChanged += (sender, e) =>
            {
                if (e.ChangedAction == ChangedAction.Add)
                {
                    var expenseViewModel = viewModelFactory.CreateCategoryStatisticViewModel(e.DataItem);
                    expenseCategoryStatistics.Add(expenseViewModel);
                }
                else if (e.ChangedAction == ChangedAction.Remove)
                {
                    foreach (ViewModel expenseViewModel in expenseCategoryStatistics)
                    {
                        if (((CatagoryStatisticViewModel)expenseViewModel).Name.Equals(e.DataItem.Name))
                        {
                            expenseCategoryStatistics.Remove(expenseViewModel);
                            break;
                        }
                    }
                }
            };

            PropertyChanged += (sender, e) =>
              {
                  if (e.PropertyName.Equals(nameof(SelectedCategory)))
                  {
                      selectedManager.SelectedExpense = selectedCategory.Expense;
                  }
              };
        }

        public IEnumerable<ViewModel> ExpenseCategoryStatistics => expenseCategoryStatistics;

        public CatagoryStatisticViewModel SelectedCategory
        {
            get => selectedCategory;
            set
            {
                SetProperty(ref selectedCategory, value);
            }
        }

        private void LoadFromExpenseProvider()
        {
            foreach (Expense expense in expenseProvider.Items)
            {
                var expenseViewModel = viewModelFactory.CreateCategoryStatisticViewModel(expense);
                expenseCategoryStatistics.Add(expenseViewModel);
            }
        }
    }
}