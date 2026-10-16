using Microsoft.EntityFrameworkCore;

namespace Sevillian.Database
{

    public class HabitContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Habits> Habits => Set<Habits>();
        public DbSet<HabitDates> HabitDates => Set<HabitDates>();
        
    }
    
    // Tables
    public class Habits
    {
        public int Id { get; set; }
        public ICollection<HabitDates> HabitDates { get; } = [];
        public required string Name { get; set; }
        public string? Description { get; set; }
    }

    public class HabitDates
    {
        public  int Id { get; set; }
        public int HabitId { get; set; }
        public Habits Habits { get; set; } = null!;
        public required DateTime PerformanceDate { get; set; }

    }
}