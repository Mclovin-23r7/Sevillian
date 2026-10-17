using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Sevillian.Database
{
    public class DatabaseManager
    {
        private readonly DbContextOptions<HabitContext> _options;
        private readonly ILogger _logger;

        public DatabaseManager(DbContextOptions<HabitContext> options)
        {
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = factory.CreateLogger("DatabaseManager");

            _options = options;
        }

        public void Initialise()
        {
            using var db = new HabitContext(_options);
            db.Database.EnsureCreated();
            _logger.LogInformation("Database initialised");
        }
        public void AddHabit(string name, string desctiption)
        {
            var newHabit = new Habits
            {
                Name = name,
                Description = desctiption
            };

            using var db = new HabitContext(_options);
            var habit = db.Habits.Add(newHabit);
            db.SaveChanges();
            _logger.LogInformation("Added habit {name} with ID {newHabit.Id}", name, newHabit.Id);
        }
        public List<Habits> GetHabits()
        {
            using var db = new HabitContext(_options);
            var habits = db.Habits.ToList();
            _logger.LogInformation("Retrieved {habits.Count} item(s)", habits.Count);
            return habits;
        }
        public Habits GetHabitById(int Id)
        {
            using var db = new HabitContext(_options);
            var habit = db.Habits.Single(
                h => h.Id == Id
            );
            _logger.LogInformation("Retrieved habit with ID {habit.Id}", Id);
            return habit;
        }
        public void UpdateHabitName(int Id, string name)
        {
            using var db = new HabitContext(_options);
            db.Habits
                .Where(h => h.Id == Id)
                .ExecuteUpdate(h => h.SetProperty(
                    n => n.Name, name
                ));
            db.SaveChanges();
            _logger.LogInformation("Updated name for habit with ID {Id}", Id);
        }
        public void UpdateHabitDescription(int Id, string description)
        {
            using var db = new HabitContext(_options);
            db.Habits
                .Where(h => h.Id == Id)
                .ExecuteUpdate(h => h.SetProperty(
                    n => n.Description, description
                ));
            db.SaveChanges();
            _logger.LogInformation("Updated description for habit with ID {Id}", Id);
        }
        public void DeleteHabit(int Id)
        {
            using var db = new HabitContext(_options);
            db.Habits.Where(h => h.Id == Id).ExecuteDelete();
            _logger.LogInformation("Deleted habit with ID {Id}", Id);
        }
    }
}