// ============================================================
// MauiProgram.cs — Точка входа и Dependency Injection (DI)
// ============================================================
// DI (Dependency Injection) — шаблон, при котором объекты
// не создают зависимости сами, а получают их снаружи.
// MAUI использует встроенный DI-контейнер Microsoft.Extensions.DI.
//
// Здесь регистрируем всё приложение:
//   Services → ViewModels → Pages → MainTabbedPage
// При запуске MAUI сам создаёт нужные объекты и передаёт зависимости.
//
// Два режима регистрации:
//   AddSingleton  — один экземпляр на всё приложение (общий).
//   AddTransient  — новый экземпляр при каждом запросе.
// ============================================================

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

        // ── База данных ───────────────────────────────────────────────────────
        // Формируем путь к файлу SQLite внутри папки приложения.
        // FileSystem.AppDataDirectory — защищённая папка, уникальная для приложения.
        // Файл создаётся автоматически при первом запуске DatabaseService.
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "city.db3");

        // Singleton — одно подключение к БД на всё приложение.
        // Лямбда _ => new DatabaseService(dbPath) вызывается только один раз.
        // Все VM, которым нужна БД, получат один и тот же экземпляр DatabaseService.
        // Это важно: два разных подключения к одному SQLite-файлу = конфликты.
        builder.Services.AddSingleton<DatabaseService>(_ => new DatabaseService(dbPath));

        // ── ViewModels ────────────────────────────────────────────────────────
        // Transient — каждый раз новый VM. Подходит для страниц с состоянием,
        // которое не нужно сохранять между визитами на страницу.
        builder.Services.AddTransient<ExploreViewModel>();
        builder.Services.AddTransient<FavoritesViewModel>();

        // Singleton — SettingsViewModel хранит выбранный язык.
        // Если сделать Transient — язык сбрасывался бы при каждом переходе на Settings.
        builder.Services.AddSingleton<SettingsViewModel>();

        builder.Services.AddTransient<AdminViewModel>();

        // ── Pages ─────────────────────────────────────────────────────────────
        // Transient — новая страница при каждом открытии.
        // Когда MAUI создаёт ExplorePage, он видит что нужен ExploreViewModel,
        // находит его в контейнере и передаёт в конструктор автоматически.
        builder.Services.AddTransient<ExplorePage>();
        builder.Services.AddTransient<FavoritesPage>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<AdminPage>();

        // Singleton — TabbedPage создаётся один раз и живёт всё время приложения.
        builder.Services.AddSingleton<MainTabbedPage>();

        return builder.Build();
    }
}
