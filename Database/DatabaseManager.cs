using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Sevillian.Database
{
    public class DatabaseManager(DbContextOptions<HabitContext> options)
    {
        private readonly DbContextOptions<HabitContext> _options = options;

        public void Initialise()
        {
            using var db = new HabitContext(_options);
            db.Database.EnsureCreated();
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
        }
        public List<Habits> GetHabits()
        {
            using var db = new HabitContext(_options);
            var habits = db.Habits.ToList();
            return habits;
        }
        public Habits GetHabitById(int Id)
        {
            using var db = new HabitContext(_options);
            var habit = db.Habits.Single(
                h => h.Id == Id
            );
            return habit;
        }
        void UpdateHabitName(int Id, string name)
        {
            using var db = new HabitContext(_options);
            db.Habits
                .Where(h => h.Id == Id)
                .ExecuteUpdate(h => h.SetProperty(
                    n => n.Name, name
                ));
        }
        void UpdateHabitDescription(int Id, string description)
        {
            using var db = new HabitContext(_options);
            db.Habits
                .Where(h => h.Id == Id)
                .ExecuteUpdate(h => h.SetProperty(
                    n => n.Description, description
                ));
        }
        void DeleteHabit(int Id)
        {
            using var db = new HabitContext(_options);
            db.Habits.Where(h => h.Id == Id).ExecuteDelete();
        }
    }
}