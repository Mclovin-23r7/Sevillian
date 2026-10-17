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
            var options = new DbContextOptionsBuilder<HabitContext>()
                .UseSqlite("Data Source=habit.db")
                .Options;

            _dbm = new DatabaseManager(options);
            _dbm.Initialise();
        }
    }
}