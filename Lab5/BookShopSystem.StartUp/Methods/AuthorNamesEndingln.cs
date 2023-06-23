using BookShopSystem.Data;
using BookShopSystem.Data.Seeding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSystem.StartUp.Methods
{
    public static class AuthorNamesEndingln
    {
        public static void Implementation()
        {
            using (var context = new BookShopSystemContext())
            {
                Console.WriteLine("Enter a string for searching authros by first name (endwith): ");

                string? input = Console.ReadLine();

                if (input == null)
                    Console.WriteLine("String is empty");

                var authors = Program.GetAuthorNamesEndingln(context, input);
                foreach (var author in authors)
                {
                    Console.WriteLine($"{author.FirstName} {author.LastName}");
                }
            }
        }
    }
}
