using BookShopSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSystem.StartUp.Methods
{
    public static class BooksByPrice
    {
        public static void Implementation()
        {
            using (var context = new BookShopSystemContext())
            {
                var books = Program.GetBooksByPrice(context);
                
                if(books == null)
                {
                    Console.WriteLine("There aren't any books with price higher than 400!");
                    Environment.Exit(0);
                }

                foreach (var book in books)
                {
                    Console.WriteLine(book.Title +  " - " + book.Price);
                }
            }
        }
    }
}
