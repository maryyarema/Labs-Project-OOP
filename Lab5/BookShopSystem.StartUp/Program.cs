using Azure.Core;
using BookShopSystem.Data;
using BookShopSystem.Models;
using BookShopSystem.StartUp.Methods;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

public static class Program
{
    private static void Main()
    {
        OutputMenu();
        int option = int.Parse(Console.ReadLine());

        while (option != 16)
        {
            ChooseMethod(option);

            OutputMenu();
            option = int.Parse(Console.ReadLine());
        }

    }
    private static void OutputMenu()
    {
        Console.WriteLine("\nChoose method: " +
               "\n\t1.AgeRestriction" +
               "\n\t2.GoldenBooks" +
               "\n\t3.BooksByPrice" +
               "\n\t4.NotReleasedIn" +
               "\n\t5.BooksTitlesByCategory" +
               "\n\t6.ReleasedBeforeDate" +
               "\n\t7.AuthorSearch" +
               "\n\t8.BookSearch" +
               "\n\t9.BookSearchByAuthor" +
               "\n\t10.CountBooks" +
               "\n\t11.TotalBookCopies" +
               "\n\t12.ProfitByCategory" +
               "\n\t13.MostRecentBooks" +
               "\n\t14.IncreasePrices" +
               "\n\t15.RemoveBooks" +
               "\n\t16.Exit");
    }
    private static void ChooseMethod(int option)
    {
        switch (option)
        {
            case 1:
                ByAgeRestriction.Implementation();
                break;
            case 2:
                GoldenBooks.Implementation();
                break;
            case 3:
                BooksByPrice.Implementation();
                break;
            case 4:
                NotReleasedIn.Implementation();
                break;
            case 5:
                BookTitleByCategory.Implementation();
                break;
            case 6:
                ReleasedBeforeDate.Implementation();
                break;
            case 7:
                AuthorNamesEndingln.Implementation();
                break;
            case 8:
                TitleBookContaining.Implementation();
                break;
            case 9:
                BooksByAuthor.Implementation();
                break;
            case 10:
                CoutnOfBooksByTitleLength.Implementation();
                break;
            case 11:
                TotalBookCopies.Implementation();
                break;
            case 12:
                TotalProfitByCategory.Implementation();
                break;
            case 13:
                MostRecentBooks.Implementation();
                break;
            case 14:
                IncreasePrices.Implementation();
                break;
            case 15:
                Remove.Implementation();
                break;
            case 16:
                Environment.Exit(0);
                break;
        }
    }
    public static IQueryable<Book>? GetBooksByAgeRestriction(BookShopSystemContext context, string? command)
    {
        switch (command.ToLower())
        {
            case "minor":
                return context.Books.Where(c => c.AgeRestriction.Equals(Book.ARType.Minor));
            case "teen":
                return context.Books.Where(c => c.AgeRestriction.Equals(Book.ARType.Teen));
            case "adult":
                return context.Books.Where(c => c.AgeRestriction.Equals(Book.ARType.Adult));
            default: return null;
        }
    }
    public static IQueryable<Book>? GetGoldenBooks(BookShopSystemContext context)
    {
        return context.Books.Where(b => b.EditionType.Equals(Book.EdType.Gold) && b.Copies < 5000).OrderBy(c => c.BookId);
    }
    public static IQueryable<Book>? GetBooksByPrice(BookShopSystemContext context)
    {
        return context.Books.Where(p => p.Price > 400.00M).OrderByDescending(c => c.Price);
    }
    public static IQueryable<Book>? GetBookNotReleasedIn(BookShopSystemContext context, int year)
    {
        return context.Books.Where(b => b.ReleaseDate.Year != year).OrderBy(c => c.BookId);
    }
    public static IQueryable<Book>? GetBooksTitlesByCategory(BookShopSystemContext context, string? input)
    {
        string[] inputArgs = input.Split(' ');
        List<Category> categories = context.Categories.ToList();
        List<int> categoryIds = new List<int>();
        List<BookCategory> bookCategories = context.BookCategory.ToList();
        List<int> booksIds = new List<int>();

        foreach (var category in categories)
        {
            foreach (var i in inputArgs)
            {
                if (category.Name.ToLower().Equals(i.ToLower()))
                {
                    categoryIds.Add(category.CategoryId);
                }
            }
        }

        foreach (var b in bookCategories)
        {
            foreach (var id in categoryIds)
            {
                if (b.CategoryId == id)
                {
                    booksIds.Add(b.BookId);
                }
            }
        }
        List<Book> booksOUT = new List<Book>();
        foreach (var bookId in booksIds)
        {
            var books = context.Books.ToList();
            foreach (var b in books)
            {
                if (b.BookId == bookId)
                {
                    booksOUT.Add(b);
                }
            }
        }

        return booksOUT.AsQueryable();
    }
    public static IQueryable<Book>? GetBooksBeforeReleaseDate(BookShopSystemContext context, string date)
    {
        string[] dateArgs = date.Split('-');
        DateTime? beforeDate = new DateTime(int.Parse(dateArgs[2]), int.Parse(dateArgs[1]), int.Parse(dateArgs[0]));

        return context.Books.Where(c => c.ReleaseDate < beforeDate).OrderBy(c => c.ReleaseDate);
    }
    public static IQueryable<Author>? GetAuthorNamesEndingln(BookShopSystemContext context, string input)
    {
        return context.Authors.Where(c => c.FirstName.EndsWith(input));
    }
    public static IQueryable<Book>? GetTitleBookContaining(BookShopSystemContext context, string input)
    {
        return context.Books.Where(c => c.Title.Contains(input)).OrderBy(c => c.Title);
    }
    public static List<Book>? GetBooksByAuthor(BookShopSystemContext context, string input)
    {
        var authors = context.Authors.Where(c => c.LastName.ToLower() == input.ToLower());

        var books = context.Books.ToList();
        List<Book> result = new List<Book>();
        foreach (var book in books)
        {
            foreach (var author in authors)
            {
                if (book.AuthorId == author.AuthorId)
                {
                    result.Add(book);
                }
            }
        }

        return result;
    }
    public static int GetCoutnOfBooksByTitleLength(BookShopSystemContext context, int lengthCheck)
    {
        var books = context.Books.Where(c => c.Title.Length > lengthCheck);
        int resultCount = books.Count();
        return resultCount;
    }
    public static void GetTotalBookCopies(BookShopSystemContext context)
    {
        var authors = context.Authors.ToList();
        var books = context.Books.ToList();

        var authorCounts = context.Books
                                        .Join(context.Authors, book => book.AuthorId, author => author.AuthorId, (book, author) => new { Book = book, Author = author })
                                        .GroupBy(x => x.Author)
                                        .ToDictionary(group => group.Key, group => group.Count())
                                        .OrderByDescending(pair => pair.Value);

        foreach (var pair in authorCounts)
        {
            Console.WriteLine("{0} {1}: {2}", pair.Key.FirstName, pair.Key.LastName, pair.Value);
        }
    }
    public static Dictionary<string, decimal> GetTotalProfitByCategory(BookShopSystemContext context)
    {
        var profitsByCategory = context.Categories
            .Select(category => new
            {
                CategoryName = category.Name,
                TotalProfit = category.BookCategories
                    .Sum(bookCategory => bookCategory.Book.Copies * bookCategory.Book.Price)
            })
            .OrderByDescending(category => category.TotalProfit)
            .ThenBy(category => category.CategoryName)
            .ToDictionary(category => category.CategoryName, category => category.TotalProfit);

        return profitsByCategory;
    }
    public static void GetMostRecentBooks(BookShopSystemContext context)
    {
        var categories = context.Categories
            .OrderByDescending(category => category.BookCategories.Count)
            .ThenBy(category => category.Name)
            .ToList();

        foreach (var category in categories)
        {
            var books = context.Books
                .Include(book => book.BookCategories)
                .Where(book => book.BookCategories.Any(bookCategory => bookCategory.CategoryId == category.CategoryId))
                .OrderByDescending(book => book.ReleaseDate)
                .Take(3)
                .ToList();

            Console.WriteLine("{0} ({1} books):", category.Name, books.Count);

            foreach (var book in books)
            {
                Console.WriteLine("- {0} ({1})", book.Title, book.ReleaseDate.Year);
            }
        }
    }
    public static void MethodIncreasePrices(BookShopSystemContext context)
    {
        var booksToUpdate = context.Books
            .Where(b => b.ReleaseDate.Year < 2010)
            .ToList();

        foreach (var book in booksToUpdate)
        {
            book.Price *= 1.05m;
        }

        context.SaveChanges();
    }
    public static int RemoveBooks(BookShopSystemContext context)
    {
        var booksToDelete = context.Books.Where(book => book.Copies < 4200).ToList();
        context.Books.RemoveRange(booksToDelete);
        context.SaveChanges();
        return booksToDelete.Count;
    }
}
