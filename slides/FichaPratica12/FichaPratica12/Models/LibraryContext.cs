using Microsoft.EntityFrameworkCore;

namespace FichaPratica12.Models
{
    // 2.d
    public class LibraryContext :DbContext
    {
        public DbSet<Book> Books { get; set; } // 2.d.i
        public DbSet<Publisher> Publishers { get; set; } // 2.d.ii

        public LibraryContext (DbContextOptions <LibraryContext> options) : base(options) { } // 2.d.iii
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // 2.d.iv
        {
            optionsBuilder.UseMySQL("server=localhost;database=library;" + "user=root;password=password;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) //2.d.v
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Publisher>(entity =>
            { 
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name);
            });
            modelBuilder.Entity<Book>(entity => 
            {
                entity.HasKey(b => b.ISBN);
                entity.Property(b => b.Title);
                entity.HasOne(p => p.Publisher);
            });
        }
    }
}
