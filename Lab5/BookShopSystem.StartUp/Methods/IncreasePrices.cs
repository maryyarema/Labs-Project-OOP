using BookShopSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSystem.StartUp.Methods
{
    public class IncreasePrices
    {
        public static void Implementation()
        {
            using (var context = new BookShopSystemContext())
            {
                Program.MethodIncreasePrices(context);
            }
        }
    }
}
