using BookShopSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSystem.StartUp.Methods
{
    public static class NotReleasedIn
    {
        public static void Implementation()
        {
            using (var context = new BookShopSystemContext())
            {
                Console.WriteLine("Enter the year: ");
                int year = int.Parse(Console.ReadLine());

                var books = Program.GetBookNotReleasedIn(context, year);

                foreach(var book in books)
                {
                    Console.WriteLine(book.Title);
                }
            }
           
        }
    }
}
