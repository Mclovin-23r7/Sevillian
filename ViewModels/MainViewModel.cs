using Sevillian.Model;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Sevillian.ViewModels;
public partial class MainViewModel : ViewModelBase
{
    private readonly HabitRepository _habitRepo = null!;
    private readonly HabitCompletionRepository _habitCompRepo = null!;
    public MainViewModel()
    {
        // Initialise Logger
        using ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var options = new DbContextOptionsBuilder<SevillianContext>()
            .UseSqlite("Data Source=habit.db")
            .EnableSensitiveDataLogging()
            .Options;

        // Initialise Model instances
        var _habitRepo = new HabitRepository(options, loggerFactory.CreateLogger<HabitRepository>());        
        var _habitCompRepo = new HabitCompletionRepository(options, loggerFactory.CreateLogger<HabitCompletionRepository>());
    }
}
