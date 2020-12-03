using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lab3.Models;


namespace Lab3.DataAccess
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseNpgsql("Host=nestor2.csc.kth.se;Database=wippich;Username=wippich;password=ReumhCR9kp7I;");

        public DbSet<Administrator> administrators { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Book> books { get; set; }
        public DbSet<Borrow> borrows { get; set; }
        public DbSet<Fine> fines { get; set; }
        public DbSet<Programme> programmes { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Genre> genres { get; set; }
        public DbSet<Publisher> publishers { get; set; }
        public DbSet<Author> authors { get; set; }
        public DbSet<Publishing> publishings { get; set; }
        public DbSet<Item> items { get; set; }
    }
}
