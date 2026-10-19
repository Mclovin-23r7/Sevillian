using Microsoft.EntityFrameworkCore;
using Sevillian.Database;

namespace Repository
{
    public abstract class Repository<T>(DbContextOptions<SevillianContext> options) where T : class
    {
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
        }
        public List<T> GetAll()
        {
            using var db = CreateContext();
            return [.. db.Set<T>().AsNoTracking()];
        }
        public T? GetById(int id)
        {
            using var db = CreateContext();
            return db.Set<T>().Find(id);
        }
    }

    public class HabitRepository(DbContextOptions<SevillianContext> options)
        : Repository<Habit>(options)
    {
        public bool UpdateNameById(int id, string name)
        {
            bool habitUpdated;
            using var db = CreateContext();
            var habit = db.Habit.Find(id);
            if (habit is null)
            {
                habitUpdated = false;
            } else
            {
                habit.Name = name;
                habitUpdated = true;
                db.SaveChanges();
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
                habitUpdated = false;
            } else
            {
                habit.Description = desc;
                habitUpdated = true;
                db.SaveChanges();
            }
            return habitUpdated;
        }
        public bool DeleteHabitById(int id)
        {
            bool habitDeleted;
            using var db = CreateContext();
            var habit = db.Habit.Find(id);
            if (habit is null)
            {
                habitDeleted = false;
            } else
            {
                db.Habit.Remove(habit);
                db.SaveChanges();
                habitDeleted = true;
            }
            return habitDeleted;
        }
    }
}