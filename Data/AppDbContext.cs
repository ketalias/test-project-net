using Microsoft.EntityFrameworkCore;
using ASPNetExapp.Models;

namespace ASPNetExapp
{
    public class AppDbContext : DbContext
    {
        public DbSet<Worker> Workers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Worker>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.LastName).IsRequired();
                entity.Property(e => e.RoomNumber).IsRequired();
                entity.Property(e => e.Department);
                entity.Property(e => e.ComputerInfo);

                entity.HasData(
                    new Worker { Id = 1, LastName = "Ivanich", RoomNumber = 15, Department = "Backend", ComputerInfo = "Asus Vivobook"},
                    new Worker { Id = 2, LastName = "Fofich", RoomNumber = 15, Department = "Frontend", ComputerInfo = "Asus Pro Vivobook"},
                    new Worker { Id = 3, LastName = "Bazzich", RoomNumber = 15, Department = "UI", ComputerInfo = "Macbook"},
                    new Worker { Id = 4, LastName = "Gedich", RoomNumber = 15, Department = "UX", ComputerInfo = "Asus Vivobook" }
                );
            });
        }
    }
}