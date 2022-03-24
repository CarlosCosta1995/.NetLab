using Microsoft.EntityFrameworkCore;

namespace Ficha14.Models
{
    public class UserContext : DbContext //Conecting the DB with the User Class throughthe DbContext
    {
        public DbSet<User> Users { get; set; } //Set User == Lista de Users

        public UserContext(DbContextOptions<UserContext> options)
        : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=Users;" +
                "user=root;password=password");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.UserName).IsRequired();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.Role).IsRequired();
                entity.Property(e => e.Email).IsRequired();
            });
        }
    }
}
