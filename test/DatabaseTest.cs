using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Sevillian.Database;

namespace Sevillian.Test
{
    public class DatabaseTests
    {
        private DatabaseManager _dbm = null!;

        [SetUp]
        public void CreateDbm()
        {
            var options = new DbContextOptionsBuilder<SevillianContext>()
                .UseSqlite("Data Source=:memory:")
                .Options;

            _dbm = new DatabaseManager(options);
            _dbm.Initialise();
            SeedHabits();
        }

        private void SeedHabits()
        {
            _dbm.AddHabit("Jork it", "Jork it daily");
            _dbm.AddHabit("Your mother", "your mother");
            _dbm.AddHabit("Daily doodles", "Daisy doodled daily");
        }

        [Test]
        public void AddHabit_NewHabit_IsPersisted()
        {
            _dbm.AddHabit("Test Habit", "Test description");
        }
    }
}