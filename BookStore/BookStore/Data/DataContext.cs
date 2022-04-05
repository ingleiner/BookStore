﻿using BookStore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class DataContext :  DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }  
        public DbSet<Book> Books { get; set; }  
        public DbSet<Genre> Genres { get; set; }    
        public DbSet<Publisher> Publishers { get; set; }    


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasIndex(b => b.Name).IsUnique();
            modelBuilder.Entity<Author>().HasIndex(a => a.Id).IsUnique();
            modelBuilder.Entity<Genre>().HasIndex(a => a.Name).IsUnique();
            modelBuilder.Entity<Publisher>().HasIndex(p => p.Name).IsUnique();


        }
    }
}
