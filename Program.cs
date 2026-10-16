/*
Program starts
Check that database exists
Create database if it doesn't
*/

using Microsoft.EntityFrameworkCore;
using Sevillian.Database;

async void AddHabit(HabitContext db, string NewName, string NewDescription )
{
    var NewHabit = new Habits
    {
        Name = NewName,
        Description = NewDescription
    };

    using (db)
    {
        var habit = await db.Habits.AddAsync(NewHabit);
        await db.SaveChangesAsync();
    }
}

async void GetHabitById(HabitContext db, int Id)
{
    using (db)
    {
        
    }
}

var options = new DbContextOptionsBuilder<HabitContext>()
    .UseSqlite("Data Source=habit.db")
    .Options;

using var db = new HabitContext(options);
db.Database.EnsureCreated();