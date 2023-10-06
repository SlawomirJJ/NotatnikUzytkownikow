using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using NotatnikUzytkownikow.Entities;

namespace NotatnikUzytkownikow
{
    public class DataContext : DbContext
    {
        private string _connectionString = "Server = (localdb)\\MSSQLLocalDB; Database=NotatnikDb; Trusted_Connection=True";
        public DbSet<User> Users { get; set; }
        public DbSet<AdditionalAttribute> AdditionalAttributes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(u =>
            {
                u.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);

                u.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(150);

                u.Property(x => x.Gender)
                .IsRequired();

                u.Property(x => x.BirthDate)
                .IsRequired();

                u.HasMany(x => x.AdditionalAttributes)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AdditionalAttribute>(a =>
            {
                a.Property(x => x.AttributeName)
                .IsRequired()
                .HasMaxLength(50);

                a.Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(200);

                a.Property(x => x.UserId)
                .IsRequired();              
            });    
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
