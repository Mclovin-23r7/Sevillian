using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Sevillian.ViewModels;
using Sevillian.Views;

namespace Sevillian;

public partial class App : Application
{
    public override void Initialize()
    {
        // ? Loads the corresponding axaml file
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // ? Assigns the starting View/Window depending on the host OS
        // ? Currently only building for desktop applications I guess
        // ? So it loads the MainWindow view with the MainViewModel view model
        // ? I guess I can load up an instance of the model in the view model
        // ? The view knows about the view-model, the view-model knows about the model but the model doesn't know either
        // ? As it were
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Start main view
            desktop.MainWindow = new MainWindow
            {
                // Connect main view model
                DataContext = new MainViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}