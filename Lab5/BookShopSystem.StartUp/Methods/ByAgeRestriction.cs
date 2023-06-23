using BookShopSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSystem.StartUp.Methods
{
    public static class ByAgeRestriction
    {
        public static void Implementation()
        {
            using (var context = new BookShopSystemContext())
            {
                Console.WriteLine("Enter a command for method GetBooksByAgeRestriction (minor, teen, adult): ");

                string? command = Console.ReadLine();

                var booksByAgeRestriction = Program.GetBooksByAgeRestriction(context, command);

                if (booksByAgeRestriction == null)
                {
                    Console.WriteLine("There are not any age restriction with this name!");
                    Environment.Exit(0);
                }

                foreach (var book in booksByAgeRestriction)
                {
                    Console.WriteLine(book.Title);
                }
            }
        }
    }
}
