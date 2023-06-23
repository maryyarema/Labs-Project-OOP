using BookShopSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSystem.StartUp.Methods
{
    public class MostRecentBooks
    {
        public static void Implementation()
        {
            using (var context = new BookShopSystemContext())
            {
                Console.WriteLine("Most recent books");
                Program.GetMostRecentBooks(context);
            }
        }
    }
}
