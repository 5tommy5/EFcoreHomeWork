using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFhomework
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Entities.Faculty> Facultys { get; set; }
        public DbSet<Entities.Group> Groups { get; set; }
        public DbSet<Entities.Student> Students { get; set; }
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Entities.Faculty>()
                .HasIndex(u => u.Name)
                .IsUnique();
            builder.Entity<Entities.Group>()
                .HasIndex(u => u.Name)
                .IsUnique();

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=super;Trusted_Connection=True;");
    }
}
