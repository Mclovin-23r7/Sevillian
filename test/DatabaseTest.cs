using Microsoft.EntityFrameworkCore;
using Sevillian.Database;
using Repository;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Sqlite;

namespace Sevillian.Test
{
    public class DatabaseTests
    {
        private HabitRepository _habitRepo = null!;
        private HabitCompletionRepository _habitCompRepo = null!;
        private readonly DateOnly _dateSeed = new(1000, 04, 23);
        
        private List<Habit> _habitSeed = [
            new Habit{Name = "Jork it", Description = "Jork it daily"},
            new Habit{Name = "Your mother", Description = "Do your mother daily"},
            new Habit{Name = "James may", Description = "Jean grey"}
        ];
        private void SeedHabits()
        {
            foreach (var c in _habitCompRepo.GetAll())
            TestContext.Out.WriteLine($"{c.HabitId} {c.PerformanceDate}");
            foreach (Habit h in _habitSeed)
            {
                _habitRepo.Add(h);
            }
        }
        private void SeedHabitCompletion()
        {
            int datesPerHabit = 3;
            var habits = _habitRepo.GetAll();
            foreach (Habit h in habits)
            {
                for (int n = 0; n < datesPerHabit; n++)
                {
                    _habitCompRepo.Add(new HabitCompletion {HabitId = h.Id, PerformanceDate = _dateSeed.AddDays(n)});
                }
            }
        }
        private int GetRandomHabitId()
        {
            return Random.Shared.Next(1, _habitSeed.Count + 1);
        }
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
            _habitCompRepo = new HabitCompletionRepository(options, loggerFactory.CreateLogger<HabitCompletionRepository>());
            _habitRepo.Initialise();
            SeedHabits();
            SeedHabitCompletion();
        }
        [Test]
        public void Add_Habit_IsPersisted()
        {
            var tName = "Test Name";
            var tDesc = "Test description";
            _habitRepo.Add(new Habit{Name = tName, Description = tDesc});
            var result = _habitRepo.Get(tName);
            Assert.That(result?.Name, Is.EqualTo(tName));
        }
        [Test]
        public void Add_HabitDate_IsPersisted()
        {
            var tId = _habitRepo.GetAll()[1].Id;
            DateOnly tDay = new(2002, 04, 23);
            _habitCompRepo.Add(new HabitCompletion {HabitId = tId, PerformanceDate = tDay});
            Assert.That(_habitCompRepo.Get(tId, tDay), Is.Not.Null);
        }
        [Test]
        public void GetAll_RetrieveAllHabits_ReturnsAllHabits()
        {
            var habitList = _habitRepo.GetAll();
            Assert.That(habitList?.Count, Is.EqualTo(_habitSeed.Count));
        }
        [Test]
        public void Get_HabitById_ReturnsCorrectHabit()
        {
            for (int c = 1; c <= _habitSeed.Count; c++)
            {
                var habit = _habitRepo.Get(c);
                Assert.That(habit?.Id, Is.EqualTo(c));
            }
        }
        [Test]
        public void Get_InvalidId_ReturnsNull()
        {
            var wrongHabit = _habitRepo.Get(_habitSeed.Count + 1);
            Assert.That(wrongHabit, Is.Null);
        }
        [Test]
        public void Get_CorrectHabitByName_ReturnsHabit()
        {
            for (int c = 1; c <= _habitSeed.Count; c++)
            {
                var habitName = _habitSeed[c-1].Name;
                var habit = _habitRepo.Get(habitName);
                Assert.That(habit?.Name, Is.EqualTo(habitName));
            }
        }
        [Test]
        public void Get_IncorrectHabitByName_ReturnsNull()
        {
            var habitName = "Test Habit Name";
            var habit = _habitRepo.Get(habitName);
            Assert.That(habit, Is.Null);
        }
        [Test]
        public void Get_HabitCompletion_ReturnsCorrectHabit()
        {
            var habitId = GetRandomHabitId();
            var habitComp = _habitCompRepo.Get(habitId, _dateSeed);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(habitComp?.HabitId, Is.EqualTo(habitId));
                Assert.That(habitComp?.PerformanceDate, Is.EqualTo(_dateSeed));
            }
        }
        [Test]
        public void Get_InvalidHabitCompletion_ReturnsNull()
        {
            var habitId = GetRandomHabitId();
            var habitComp = _habitCompRepo.Get(habitId, _dateSeed.AddDays(100));
            Assert.That(habitComp, Is.Null);
        }        
        [Test]
        public void UpdateNameById_UpdateHabit_HabitNameSuccessfullyUpdated()
        {
            int habitId = Random.Shared.Next(1, _habitSeed.Count + 1);
            string testName = "Updated Name";
            var success = _habitRepo.UpdateNameById(habitId, testName);
            var habit = _habitRepo.Get(habitId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(habit?.Id, Is.EqualTo(habitId));
                Assert.That(habit?.Name, Is.EqualTo(testName));
                Assert.That(success, Is.True);
            }
        }
        [Test]
        public void UpdateNameById_IncorrectHabitId_ReturnsFalse()
        {
            var testId = 500;
            bool response = _habitRepo.UpdateNameById(testId, "It will never work");
            Assert.That(response, Is.False);
        }
        [Test]
        public void UpdateDescById_UpdateHabit_HabitDescSuccessfullyUpdated()
        {
            int habitId = Random.Shared.Next(1, _habitSeed.Count + 1);
            string testDesc = "Updated desc";
            var success = _habitRepo.UpdateDescById(habitId, testDesc);
            var habit = _habitRepo.Get(habitId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(habit?.Id, Is.EqualTo(habitId));
                Assert.That(habit?.Description, Is.EqualTo(testDesc));
                Assert.That(success, Is.True);
            }
        }
        [Test]
        public void UpdateDescById_IncorrectHabitId_ReturnsFalse()
        {
            var testId = 500;
            bool response = _habitRepo.UpdateDescById(testId, "It will never work");
            Assert.That(response, Is.False);
        }        
        [Test]
        public void DeleteHabit_DeletingHabit_HabitIsDeleted()
        {
            int habitId = Random.Shared.Next(1, _habitSeed.Count + 1);
            bool success = _habitRepo.DeleteHabit(habitId);
            Habit? habit = _habitRepo.Get(habitId);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(success, Is.True);
                Assert.That(habit, Is.Null);
            }
        }
        [Test]
        public void DeleteHabit_IncorrectHabitId_ReturnsFalse()
        {
            int habitId = 500;
            bool response = _habitRepo.DeleteHabit(habitId);
            Assert.That(response, Is.False);
        }
    }
}