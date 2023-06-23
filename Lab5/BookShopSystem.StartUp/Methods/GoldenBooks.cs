using BookShopSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSystem.StartUp.Methods
{
    public static class GoldenBooks
    {
        public static void Implementation()
        {
            using (var context = new BookShopSystemContext())
            {
                var goldenBooks = Program.GetGoldenBooks(context);

                if (goldenBooks == null)
                {
                    Console.WriteLine("There are not any golden books that have less than 5000 copies!");
                    Environment.Exit(0);
                }

                foreach (var book in goldenBooks)
                {
                    Console.WriteLine(book.Title);
                }
            }
        }
    }
}
