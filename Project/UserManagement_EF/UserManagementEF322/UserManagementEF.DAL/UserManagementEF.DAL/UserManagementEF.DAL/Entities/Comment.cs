using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace UserManagementEF.DAL.Entities
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        [Column("comment_id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("movie_id")]
        public int MovieId { get; set; }

        [Required]
        [Column("comment_text")]
        public string CommentText { get; set; }

        [Required]
        [Column("comment_date")]
        public DateTime CommentDate { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } // many-to-one

        [ForeignKey("MovieId")]
        public Movie Movie { get; set; } // many-to-one
    }
}
