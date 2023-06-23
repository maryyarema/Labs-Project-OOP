using BookShopSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSystem.StartUp.Methods
{
    public static class BookTitleByCategory
    {
        public static void Implementation()
        {
            using (var context = new BookShopSystemContext())
            {
                Console.WriteLine("Enter an categories in single line separeted by space: ");
                string? input = Console.ReadLine();

                var books = Program.GetBooksTitlesByCategory(context, input);
                
                if(books == null)
                {
                    Console.WriteLine("There aren't any books with these categories!");
                    Environment.Exit(0);
                }

                foreach (var book in books)
                {
                    Console.WriteLine(book.Title);
                }
            }
        }
    }
}
