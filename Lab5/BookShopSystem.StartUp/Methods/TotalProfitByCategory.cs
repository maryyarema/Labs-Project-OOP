using BookShopSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSystem.StartUp.Methods
{
    public class TotalProfitByCategory
    {
        public static void Implementation()
        {
            using (var context = new BookShopSystemContext())
            {
                Dictionary<string, decimal> profitsByCategory = Program.GetTotalProfitByCategory(context);

                Console.WriteLine("Total profit of all books by category");
                foreach (var category in profitsByCategory)
                {
                    Console.WriteLine("{0}: {1:C}", category.Key, category.Value);
                }
            }
        }
    }
}
