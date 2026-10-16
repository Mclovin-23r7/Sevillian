/*
Program starts
Check that database exists
Create database if it doesn't
*/

using Microsoft.EntityFrameworkCore;
using Sevillian.Database;

var options = new DbContextOptionsBuilder<HabitContext>()
    .UseSqlite("Data Source=habit.db")
    .Options;

var dbm = new DatabaseManager(options);
dbm.Initialise();