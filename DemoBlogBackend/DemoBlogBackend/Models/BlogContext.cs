﻿using Microsoft.EntityFrameworkCore;

namespace DemoBlogBackend.Models
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
           : base(options)
        { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PersonalRating> PersonalRatings { get; set; }
        public DbSet<Mark> Marks { get; set; }
    }
}
