using Microsoft.EntityFrameworkCore;
using MVCBookShop.Models;

namespace MVCBookShop
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
        {
            Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost; Port=5432; Database=booksweb; User Id = postgres; Password= 29032004;TrustServerCertificate=True;");
        }

        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Writing> Writing { get; set; }
        public DbSet<WritingBook> WritingBook { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Order> Order { get; set; }
    }
}
