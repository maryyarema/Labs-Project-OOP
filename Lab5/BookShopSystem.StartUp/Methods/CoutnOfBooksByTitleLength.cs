using BookShopSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSystem.StartUp.Methods
{
    public class CoutnOfBooksByTitleLength
    {
        public static void Implementation()
        {
            using (var context = new BookShopSystemContext())
            {
                Console.WriteLine("Enter a length of title which I would use in search: ");
                int length = int.Parse(Console.ReadLine());

                var booksCount = Program.GetCoutnOfBooksByTitleLength(context, length);
                Console.WriteLine($"There are {booksCount} books with title longer than {length}");
            }

        }
    }
}
