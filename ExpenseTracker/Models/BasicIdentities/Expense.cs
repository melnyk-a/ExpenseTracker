namespace ExpenseTracker.Models.BasicIdentities
{
    internal sealed class Expense : BasicIdentity
    {
        private string imagePath;
        private int limit;

        public Expense(string name) : 
            base(name)
        {
        }

        public string ImagePath
        {
            get => imagePath;
            set => SetProperty(ref imagePath, value);
        }

        public override int Value
        {
            get => limit;
            set => SetProperty(ref limit, value);
        }
    }
}