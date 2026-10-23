using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sevillian.Database;

namespace Repository
{
    public abstract class Repository<T>(
        DbContextOptions<SevillianContext> options,
        ILogger logger
        ) where T : class
    {
        protected ILogger EL => logger;
        protected SevillianContext CreateContext() => new(options);
        public void Initialise()
        {
            using var db = CreateContext();
            db.Database.Migrate();
        }
        public void Add(T entity)
        {
            using var db = CreateContext();
            db.Set<T>().Add(entity);
            db.SaveChanges();
            EL.LogInformation("Added {entity}", typeof(T).Name);
        }
        public List<T> GetAll()
        {
            using var db = CreateContext();
            EL.LogInformation("Retrieved all {entity}", typeof(T));
            return [.. db.Set<T>().AsNoTracking()];
        }
        public T? Get(int id)
        {
            using var db = CreateContext();
            EL.LogInformation("Retrieved {entity} with ID {id}", typeof(T), id);
            return db.Set<T>().Find(id);
        }
    }

    public class HabitRepository(
        DbContextOptions<SevillianContext> options,
        ILogger logger
        )
        : Repository<Habit>(options, logger)
    {
        public Habit? Get(string name)
        {
            using var db = CreateContext();
            var habit = db.Habit.AsNoTracking().FirstOrDefault(h => h.Name == name);
            if (habit is null)
            {
                EL.LogWarning("Failed to retrieve {entity} with name {name}", typeof(Habit), name);
                return null;
            } else
            {
                EL.LogInformation("Retrieved {entity} with name {name}", typeof(Habit), name);
                return habit;
            }
        }
        public bool UpdateNameById(int id, string name)
        {
            bool habitUpdated;
            using var db = CreateContext();
            var habit = Get(id);
            if (habit is null)
            {
                EL.LogWarning("Failed to find habit with ID {}", id);
                habitUpdated = false;
            } else
            {
                habit.Name = name;
                habitUpdated = true;
                db.SaveChanges();
                EL.LogInformation("Updated habit ID {id} with name {name}", id, name);
            }
            return habitUpdated;
        }
        public bool UpdateDescById(int id, string desc)
        {
            bool habitUpdated;
            using var db = CreateContext();
            var habit = db.Habit.Find(id);
            if (habit is null)
            {
                EL.LogWarning("Failed to find habit with ID {}", id);
                habitUpdated = false;
            } else
            {
                habit.Description = desc;
                habitUpdated = true;
                db.SaveChanges();
                EL.LogInformation("Updated description for habit ID {id}", id);
            }
            return habitUpdated;
        }
        public bool DeleteHabit(int id)
        {
            bool habitDeleted;
            using var db = CreateContext();
            var habit = db.Habit.Find(id);
            if (habit is null)
            {
                EL.LogWarning("Failed to find habit with ID {}", id);
                habitDeleted = false;
            } else
            {
                db.Habit.Remove(habit);
                db.SaveChanges();
                habitDeleted = true;
                EL.LogInformation("Deleted habit with ID {id}", id);
            }
            return habitDeleted;
        }
    }
}