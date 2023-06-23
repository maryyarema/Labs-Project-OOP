﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagementEF.DAL.Entities;

namespace UserManagementEF.DAL.Data.Configurations
{
    public class CommentsConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .HasKey(c => c.CommentId);

            builder // one-to-many  Comments - Users
               .HasOne(c => c.User)
               .WithMany(u => u.Ratings)
               .HasForeignKey(c => c.UserId);

            builder // one-to-many  Comments Movies - 
               .HasOne(c => c.Movie)
                .WithMany(m => m.Comments)
                .HasForeignKey(c => c.MovieId);


        }
    }
}
