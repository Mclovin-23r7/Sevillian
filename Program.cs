/*
Program starts
Check that database exists
Create database if it doesn't
*/
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Sevillian.Database;
using Repository;
using Avalonia;

// using ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
// var options = new DbContextOptionsBuilder<SevillianContext>()
//     .UseSqlite("Data Source=habit.db")
//     .EnableSensitiveDataLogging()
//     .Options;

// var habitRepo = new HabitRepository(options, loggerFactory.CreateLogger<HabitRepository>());

namespace Sevillian;

sealed class Program
{
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();
}