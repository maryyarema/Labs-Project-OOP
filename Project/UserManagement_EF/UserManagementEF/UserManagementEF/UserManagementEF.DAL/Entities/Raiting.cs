using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementEF.UserManagementEF.DAL.Entities
{
    [Table("Ratings")]
    public class Rating
    {
        [Key]
        [Column("rating_id")]
        public int RatingId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("movie_id")]
        public int MovieId { get; set; }


        [Column("rating")]
        public int Raiting { get; set; }

        [Required]
        [Column("rating_date")]
        public DateTime RatingDate { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } // many-to-one

        [ForeignKey("MovieId")]
        public Movie Movie { get; set; } // many-to-one
    }
}







