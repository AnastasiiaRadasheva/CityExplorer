
using System.Collections.ObjectModel;
using System.Windows.Input;
using CityExplorer.Models;
using CityExplorer.Services;

namespace CityExplorer.ViewModels;

public class AdminViewModel : BaseViewModel
{
    private readonly DatabaseService _db;

    public ObservableCollection<Place> AllPlaces { get; } = new();

    public ICommand DeleteCommand { get; }

    public AdminViewModel(DatabaseService db)
    {
        _db = db;
        DeleteCommand = new Command<Place>(async p => await DeletePlaceAsync(p));
    }

    public async Task LoadAsync()
    {
        AllPlaces.Clear();

        
        var list = await _db.GetAllPlacesAsync();

        foreach (var p in list)
        {
            p.ApplyCardColor(); 
            AllPlaces.Add(p);
        }
    }

    private async Task DeletePlaceAsync(Place place)
    {
        await _db.DeletePlaceAsync(place);

        AllPlaces.Remove(place);
    }
}
