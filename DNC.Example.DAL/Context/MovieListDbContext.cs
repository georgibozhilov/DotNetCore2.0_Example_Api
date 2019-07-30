namespace DNC.Example.DAL.Context
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.EntityFrameworkCore;
    
    using DNC.Example.DAL.Models;

    public class MovieListDbContext : DbContext
    {
        public MovieListDbContext(DbContextOptions<MovieListDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Movie> Movies { get; set; }

    }
}

