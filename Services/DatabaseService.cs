

using SQLite;
using CityExplorer.Data;
using CityExplorer.Models;

namespace CityExplorer.Services;

public class DatabaseService
{
  
    private readonly SQLiteAsyncConnection _db;

    public DatabaseService(string dbPath)
    {
        _db = new SQLiteAsyncConnection(dbPath);

        Task.Run(async () =>
        {
          
            await _db.CreateTableAsync<Place>();

            await SeedAsync();
        }).GetAwaiter().GetResult();
    }

    private async Task SeedAsync()
    {

        if (Preferences.Default.Get("db_seeded_v2", false)) return;

        foreach (var p in DefaultPlaces.All)
            await _db.InsertAsync(p);

        Preferences.Default.Set("db_seeded_v2", true);
    }

    public Task<List<Place>> GetAllPlacesAsync() =>
        _db.Table<Place>().ToListAsync();

    public Task<List<Place>> GetPlacesByCategoryAsync(string emoji) =>
        _db.Table<Place>().Where(p => p.CategoryEmoji == emoji).ToListAsync();

    public Task<List<Place>> GetFavoritesAsync() =>
        _db.Table<Place>().Where(p => p.IsFavorite).ToListAsync();

    public Task<int> SavePlaceAsync(Place place) =>
        place.Id == 0 ? _db.InsertAsync(place) : _db.UpdateAsync(place);

    public Task<int> DeletePlaceAsync(Place place) =>
        _db.DeleteAsync(place);
}
