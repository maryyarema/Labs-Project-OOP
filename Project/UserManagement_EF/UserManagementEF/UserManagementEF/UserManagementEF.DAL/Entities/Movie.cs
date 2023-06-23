using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace UserManagementEF.UserManagementEF.DAL.Entities
{
    [Table("Movies", Schema = "MovieManagement")]
    public class Movie
    {
        [Key]
        [Column("movie_id")]
        public int MovieId { get; set; }

        [Required]
        [Column("title")]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        [Column("release_year")]
        public int ReleaseYear { get; set; }

        [Required]
        [Column("director")]
        [MaxLength(255)]
        public string Director { get; set; }

        [Required]
        [Column("description")]
        public string Description { get; set; }

        [Column("duration")]
        public int Duration { get; set; }

        public ICollection<Comment> Comments { get; set; } // one-to-many
        public ICollection<Rating> Ratings { get; set; } // one-to-many
    }
}
