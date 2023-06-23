using BookShopSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSystem.StartUp.Methods
{
    public static class TitleBookContaining
    {
        public static void Implementation()
        {
            using (var context = new BookShopSystemContext())
            {
                Console.WriteLine("Enter letters to find titles of books: ");
                
                string? input = Console.ReadLine();

                if (input == null) { Console.WriteLine("String is empty"); }

                var books = Program.GetTitleBookContaining(context, input);

                foreach (var book in books)
                {
                    Console.WriteLine(book.Title);
                }
            }
        }
    }
}
