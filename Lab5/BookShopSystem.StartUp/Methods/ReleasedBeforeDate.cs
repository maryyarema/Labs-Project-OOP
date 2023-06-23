using BookShopSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSystem.StartUp.Methods
{
    public static class ReleasedBeforeDate
    {
        public static void Implementation()
        {
            using (var context = new BookShopSystemContext())
            {
                Console.WriteLine("Enter the date: ");
                string? date = Console.ReadLine();

                var books = Program.GetBooksBeforeReleaseDate(context, date);
                foreach(var book in books)
                    Console.WriteLine(book.Title + " - " + book.EditionType + " - " + book.Price);
            }
        }
    }
}
