using System;

namespace ExpenseTracker.Models.BasicIdentities
{
    internal sealed class Bill : BasicIdentity
    {
        private string accountName;
        private DateTime date;
        private string expenseCategoryName;
        private string imagePath;
        private int sum;

        public Bill(string name) : base(name)
        {
        }

        public string AccountName
        {
            get => accountName;
            set => SetProperty(ref accountName, value);
        }

        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public string ExpenseCategoryName
        {
            get => expenseCategoryName;
            set => SetProperty(ref expenseCategoryName, value);
        }

        public string ImagePath
        {
            get => imagePath;
            set => SetProperty(ref imagePath, value);
        }

        public override int Value
        {
            get => sum;
            set => SetProperty(ref sum, value);
        }
    }
}