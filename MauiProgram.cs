using CityExplorer.Services;
using CityExplorer.ViewModels;
using CityExplorer.Views;
using Microsoft.Extensions.Logging;

namespace CityExplorer;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "city.db3");
        builder.Services.AddSingleton<DatabaseService>(_ => new DatabaseService(dbPath));

        builder.Services.AddTransient<ExploreViewModel>();
        builder.Services.AddTransient<FavoritesViewModel>();
        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddTransient<AdminViewModel>();

        builder.Services.AddTransient<ExplorePage>();
        builder.Services.AddTransient<FavoritesPage>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<AdminPage>();
        builder.Services.AddSingleton<MainTabbedPage>();

        return builder.Build();
    }
}
