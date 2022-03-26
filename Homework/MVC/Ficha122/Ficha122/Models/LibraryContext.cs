using Microsoft.EntityFrameworkCore;

namespace Ficha122.Models
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        //Instancia do LibraryContext usando as propriedades do DbContext
        public LibraryContext (DbContextOptions<LibraryContext> options) : base(options) 
        {
        }

        //Options de set para fazer a ligação na DB[Configuração da DB] (NomeServidor, NomeDB, User e Password)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=library;" + "user=root;password=password;");
        }

        //Criação do Modelo das tabelas base na DB
        protected override void OnModelCreating(ModelBuilder molderBuilder)
        {
            base.OnModelCreating(molderBuilder);

            molderBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(book => book.ISBN);
                entity.Property(book => book.Title).IsRequired();
                entity.HasOne(pub => pub.Publisher);
            });

            molderBuilder.Entity<Publisher>(entity =>
            {
                entity.HasKey(p => p.ID);
                entity.Property(p => p.Name).IsRequired();
            });
        }
    }
}
