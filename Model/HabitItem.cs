namespace Sevillian.Model;

// Encapsulates a single Habit from the database
public class HabitItem
{
    public required int Id { get; set; }
    public required string HabitName { get; set; }
    public string? HabitDesc { get; set; }

}