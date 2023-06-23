using BookShopSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSystem.StartUp.Methods
{
    public static class BooksByAuthor
    {
        public static void Implementation()
        {
            using (var context = new BookShopSystemContext())
            {
                Console.WriteLine("Enter string of author's last name to get titles of books: ");

                string? input = Console.ReadLine();

                if (input == null) Console.WriteLine("String is empty");

                var books = Program.GetBooksByAuthor(context, input);

                foreach (var book in books)
                {
                    Console.WriteLine($"{book.Title} ({book.Author.FirstName} {book.Author.LastName})");
                }
            }
        }
    }
}
