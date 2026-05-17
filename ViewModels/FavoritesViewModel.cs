

using System.Collections.ObjectModel;
using System.Windows.Input;
using CityExplorer.Models;
using CityExplorer.Resources.Strings;
using CityExplorer.Services;

namespace CityExplorer.ViewModels;

public class FavoritesViewModel : BaseViewModel
{
    private readonly DatabaseService _db;

  
    public ObservableCollection<Place> Favorites { get; } = new();

    public ICommand RemoveCommand      { get; }
    public ICommand ShowDetailsCommand { get; }

    public FavoritesViewModel(DatabaseService db)
    {
        _db = db;
        RemoveCommand      = new Command<Place>(async p => await RemoveFavoriteAsync(p));
        ShowDetailsCommand = new Command<Place>(async p => await ShowDetailsAsync(p));
    }

    public async Task LoadFavoritesAsync()
    {
        Favorites.Clear();

        var list = await _db.GetFavoritesAsync();

        foreach (var p in list)
        {
            p.ApplyCardColor(); 
            Favorites.Add(p);
        }
    }

    private async Task RemoveFavoriteAsync(Place place)
    {
        place.IsFavorite = false;

        await _db.SavePlaceAsync(place);

        Favorites.Remove(place);
    }

    private static Page CurrentPage => Application.Current!.Windows[0].Page!;

    private async Task ShowDetailsAsync(Place place)
    {
        var closeBtn  = LocalizationResourceManager.Instance["Alert_Close"]?.ToString() ?? "Sulge";
        var addrLbl   = LocalizationResourceManager.Instance["Alert_Address"]?.ToString() ?? "📍";
        var removeBtn = LocalizationResourceManager.Instance["Swipe_Remove"]?.ToString() ?? "Eemalda";

        var details  = $"{place.FullDescription}\n\n{addrLbl}: {place.Address}";

        var doRemove = await CurrentPage.DisplayAlertAsync(place.Name, details, removeBtn, closeBtn);

        if (doRemove)
            await RemoveFavoriteAsync(place);
    }
}
