using System.Collections.Generic;

namespace ExpenseTracker.Models
{
    internal static class AccountImages
    {
        private static ICollection<string> accountImages = new List<string>();

        static AccountImages()
        {
            accountImages.Add(@"..\..\Resources\CreditCard.png");
            accountImages.Add(@"..\..\Resources\Internet.png");
            accountImages.Add(@"..\..\Resources\Jewelry.png");
            accountImages.Add(@"..\..\Resources\Sales.png");
            accountImages.Add(@"..\..\Resources\Wallet.png");
        }

        public static IEnumerable<string> Paths => accountImages;
    }
}