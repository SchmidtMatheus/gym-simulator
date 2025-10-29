using GymSimulator.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace GymSimulator.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<PlanType> PlanTypes => Set<PlanType>();
    public DbSet<ClassType> ClassTypes => Set<ClassType>();
    public DbSet<Class> Classes => Set<Class>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<BookingHistory> BookingHistories => Set<BookingHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Booking>()
            .HasIndex(b => new { b.StudentId, b.ClassId })
            .IsUnique();

        modelBuilder.Entity<PlanType>()
            .HasIndex(p => p.Name)
            .IsUnique();

        modelBuilder.Entity<ClassType>()
            .HasIndex(c => c.Name)
            .IsUnique();
    }
}
