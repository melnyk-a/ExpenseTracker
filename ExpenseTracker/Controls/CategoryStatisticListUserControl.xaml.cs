using System.Windows.Controls;
using System.Windows.Media;

namespace ExpenseTracker.Controls
{
    internal partial class CategoryStatisticListUserControl : UserControl
    {
        public CategoryStatisticListUserControl()
        {
            InitializeComponent();
        }

        private void ListBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            HitTestResult r = VisualTreeHelper.HitTest(this, e.GetPosition(this));
            if (r.VisualHit.GetType() != typeof(ListBoxItem))
            {
                (e.Source as ListBox).UnselectAll();
            }
        }
    }
}