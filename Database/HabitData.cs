using Sevillian.Database;
using Microsoft.EntityFrameworkCore;

namespace HabitData
{
    public class HabitDb(DbContextOptions<HabitDb> options) : DbContext(options)
    {
        public DbSet<Habits> Habits { get; set; }
        public DbSet<HabitDates> HabitDates { get; set; }
        
    }
}