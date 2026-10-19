/*
Program starts
Check that database exists
Create database if it doesn't
*/
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Sevillian.Database;
using Repository;

using ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
var options = new DbContextOptionsBuilder<SevillianContext>()
    .UseSqlite("Data Source=habit.db")
    .EnableSensitiveDataLogging()
    .Options;

var habitRepo = new HabitRepository(options, loggerFactory.CreateLogger<HabitRepository>());