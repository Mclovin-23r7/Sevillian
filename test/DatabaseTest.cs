using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Sevillian.Database;
using Repository;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Sqlite;

namespace Sevillian.Test
{
    public class DatabaseTests
    {
        private HabitRepository _habitRepo = null!;

        [SetUp]
        public void CreateRepos()
        {
            using ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<SevillianContext>()
                .UseSqlite(_connection)
                .Options;

            _habitRepo = new HabitRepository(options, loggerFactory.CreateLogger<HabitRepository>());
            _habitRepo.Initialise();
            SeedHabits();
        }

        private void SeedHabits()
        {
            _habitRepo.Add(new Habit{Name = "Jork it", Description = "Jork it daily"});
            _habitRepo.Add(new Habit{Name = "Your mother", Description = "Do your mother daily"});
            _habitRepo.Add(new Habit{Name = "James may", Description = "Jean grey"});
        }

        [Test]
        public void AddHabit_NewHabit_IsPersisted()
        {
            var tName = "Test Name";
            var tDesc = "Test description";
            _habitRepo.Add(new Habit{Name = tName, Description = tDesc});
            _habitRepo.Get(tName);
        }
    }
}