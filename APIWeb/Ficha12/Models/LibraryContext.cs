using Ficha12.Models;
using Microsoft.EntityFrameworkCore;

namespace Ficha12
{
    public class LibraryContext : DbContext
    {
        //Define properties
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        //Create parameter contructor
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        //Overrride the OnConfiguring Method
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseMySQL("server=localhost;database=library;" + "user=root; password=password");
        }

        //Overrride the OnConfiguring Method
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            //Instanciate the base OnModelCreating
            base.OnModelCreating(modelBuilder);

            //Defining the PrimaryKey, ForeignKey and properties
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.ISBN);
                entity.Property(e => e.Title).IsRequired();
                entity.HasOne(p => p.Publisher);
            });

            modelBuilder.Entity<Publisher>(entity => 
            { 
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired();
            });
        }
    }
}
