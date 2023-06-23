using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSystem.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }
        public int Copies { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public EdType EditionType { get; set; }
        public ARType AgeRestriction { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }

        public enum EdType
        {
            Normal, Promo, Gold
        }
        public enum ARType
        {
            Minor, Teen, Adult
        }
    }
}
