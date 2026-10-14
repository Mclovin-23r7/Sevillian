namespace Sevillian.Database
{
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
        public Habits HabitId { get; set; } = null!;
        public required DateTime PerformanceDate { get; set; }
    }
}