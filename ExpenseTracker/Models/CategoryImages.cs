using System.Collections.Generic;

namespace ExpenseTracker.Models
{
    internal static class CategoryImages
    {
        private static ICollection<string> categoryImages = new List<string>();

        static CategoryImages()
        {
            categoryImages.Add(@"..\..\Resources\Account.png");
            categoryImages.Add(@"..\..\Resources\Award.png");
            categoryImages.Add(@"..\..\Resources\Brush.png");
            categoryImages.Add(@"..\..\Resources\Car.png");
            categoryImages.Add(@"..\..\Resources\Cart.png");
            categoryImages.Add(@"..\..\Resources\Checklist.png");
            categoryImages.Add(@"..\..\Resources\Compass.png");
            categoryImages.Add(@"..\..\Resources\Direction.png");
            categoryImages.Add(@"..\..\Resources\Eye.png");
            categoryImages.Add(@"..\..\Resources\Flask.png");
            categoryImages.Add(@"..\..\Resources\Internet.png");
            categoryImages.Add(@"..\..\Resources\Jewelry.png");
            categoryImages.Add(@"..\..\Resources\Key.png");
            categoryImages.Add(@"..\..\Resources\Mail.png");
            categoryImages.Add(@"..\..\Resources\Mountain.png");
            categoryImages.Add(@"..\..\Resources\Phone.png");
        }

        public static IEnumerable<string> Paths => categoryImages;
    }
}