/*
Program starts
Check that database exists
Create database if it doesn't
*/
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Sevillian.Database;

using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
ILogger logger = factory.CreateLogger("Program");

var options = new DbContextOptionsBuilder<HabitContext>()
    .UseSqlite("Data Source=habit.db")
    .Options;

var dbm = new DatabaseManager(options);
dbm.Initialise();
dbm.AddHabit("Jork it", "Jork it daily");
dbm.AddHabit("Your mother", "your mother");
dbm.AddHabit("Daily doodles", "Daisy doodled daily");
var habits = dbm.GetHabits();
Console.WriteLine(habits.Count);
dbm.UpdateHabitDescription(habits[^1].Id, "Updated description");
dbm.UpdateHabitName(habits[^1].Id, "Updated name");
dbm.DeleteHabit(habits[^1].Id);
Console.WriteLine(habits.Count);