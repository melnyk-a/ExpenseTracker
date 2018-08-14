using System;

namespace ExpenseTracker.Models.BasicIdentities
{
    internal sealed class Account : BasicIdentity
    {
        private int funds;
        private string imagePath;

        public Account(string name) : 
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
            get => funds;
            set => SetProperty(ref funds, value);
        }
    }
}