using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Sevillian.Model;

public class SevillianContextFactory : IDesignTimeDbContextFactory<SevillianContext>
{
    public SevillianContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<SevillianContext>()
            .UseSqlite("Data Source=:memory:")
            .Options;
        return new SevillianContext(options);
    }
}
public class SevillianContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Habit> Habit => Set<Habit>();
    public DbSet<HabitCompletion> HabitCompletion => Set<HabitCompletion>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Habit>(e =>
        {
            e.HasIndex(e => e.Id);
            e.Property(e => e.Name).IsRequired().HasMaxLength(100);
            e.Property(e => e.Description).HasMaxLength(500);
            e.HasMany(h => h.HabitCompletion)
            .WithOne(d => d.Habit)
            .HasForeignKey(d => d.HabitId)
            .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<HabitCompletion>(e =>
        {
            e.HasKey(e => new { e.HabitId, e.PerformanceDate });
        });
    }
}

// Tables
public class Habit
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<HabitCompletion> HabitCompletion { get; } = [];
}

public class HabitCompletion
{
    public  int Id { get; set; }
    public int HabitId { get; set; }
    public Habit Habit { get; set; } = null!;
    public required DateOnly PerformanceDate { get; set; }

}