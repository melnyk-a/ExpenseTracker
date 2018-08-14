using ExpenseTracker.ViewModels;
using System.Windows;

namespace ExpenseTracker.Views
{
    internal partial class MainWindowView : Window
    {
        public MainWindowView(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}