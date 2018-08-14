namespace ExpenseTracker.Attributes
{
    internal sealed class RaiseCanExecuteDependsUponAttribute: DependsUponAttribute
    {
        public RaiseCanExecuteDependsUponAttribute(string propertyName) :
            base(propertyName)
        {
        }
    }
}