using System.Collections.ObjectModel;
using System.Windows.Input;
using CityExplorer.Models;
using CityExplorer.Resources.Strings;
using CityExplorer.Services;

namespace CityExplorer.ViewModels;

public class FavoritesViewModel : BaseViewModel
{
    private readonly DatabaseService _db;

    // Полный список всех избранных из БД — не показывается напрямую.
    // Favorites — отфильтрованный вид, который биндится к CollectionView.
    private List<Place> _allFavorites = new();
    public ObservableCollection<Place> Favorites { get; } = new();

    // Текущая выбранная категория-фильтр. Пустая строка = "Все".
    private string _selectedCategory = string.Empty;
    public string SelectedCategory
    {
        get => _selectedCategory;
        set
        {
            if (SetProperty(ref _selectedCategory, value))
            {
                ApplyFilter();
                OnPropertyChanged(nameof(AllBtnColor));
                OnPropertyChanged(nameof(HistoryBtnColor));
                OnPropertyChanged(nameof(ParksBtnColor));
                OnPropertyChanged(nameof(FoodBtnColor));
                OnPropertyChanged(nameof(AllTextColor));
                OnPropertyChanged(nameof(HistoryTextColor));
                OnPropertyChanged(nameof(ParksTextColor));
                OnPropertyChanged(nameof(FoodTextColor));
            }
        }
    }

    private static readonly Color Inactive   = Color.FromArgb("#2e1406");
    private static readonly Color TextActive = Colors.White;
    private static readonly Color TextInact  = Color.FromArgb("#c07848");

    public Color AllBtnColor     => string.IsNullOrEmpty(_selectedCategory) ? Color.FromArgb("#ea580c") : Inactive;
    public Color HistoryBtnColor => _selectedCategory == "🏰" ? Color.FromArgb("#7c2d12") : Inactive;
    public Color ParksBtnColor   => _selectedCategory == "🌳" ? Color.FromArgb("#14532d") : Inactive;
    public Color FoodBtnColor    => _selectedCategory == "🍽️" ? Color.FromArgb("#1e3a8a") : Inactive;

    public Color AllTextColor     => string.IsNullOrEmpty(_selectedCategory) ? TextActive : TextInact;
    public Color HistoryTextColor => _selectedCategory == "🏰" ? TextActive : TextInact;
    public Color ParksTextColor   => _selectedCategory == "🌳" ? TextActive : TextInact;
    public Color FoodTextColor    => _selectedCategory == "🍽️" ? TextActive : TextInact;

    public ICommand RemoveCommand         { get; }
    public ICommand ShowDetailsCommand    { get; }
    public ICommand SelectCategoryCommand { get; }

    public FavoritesViewModel(DatabaseService db)
    {
        _db = db;
        RemoveCommand         = new Command<Place>(async p => await RemoveFavoriteAsync(p));
        ShowDetailsCommand    = new Command<Place>(async p => await ShowDetailsAsync(p));
        SelectCategoryCommand = new Command<string>(cat => SelectedCategory = cat);
    }

    // Загружает все избранные из БД, затем применяет текущий фильтр.
    public async Task LoadFavoritesAsync()
    {
        _allFavorites.Clear();
        var list = await _db.GetFavoritesAsync();
        foreach (var p in list)
        {
            p.ApplyCardColor();
            _allFavorites.Add(p);
        }
        ApplyFilter();
    }

    // Перестраивает Favorites по текущему SelectedCategory.
    private void ApplyFilter()
    {
        Favorites.Clear();
        var source = string.IsNullOrEmpty(_selectedCategory)
            ? _allFavorites
            : _allFavorites.Where(p => p.CategoryEmoji == _selectedCategory);
        foreach (var p in source)
            Favorites.Add(p);
    }

    private async Task RemoveFavoriteAsync(Place place)
    {
        place.IsFavorite = false;
        await _db.SavePlaceAsync(place);
        _allFavorites.Remove(place);
        Favorites.Remove(place);
    }

    private static Page CurrentPage => Application.Current!.Windows[0].Page!;

    private async Task ShowDetailsAsync(Place place)
    {
        var closeBtn  = LocalizationResourceManager.Instance["Alert_Close"]?.ToString()  ?? "Sulge";
        var addrLbl   = LocalizationResourceManager.Instance["Alert_Address"]?.ToString() ?? "📍";
        var removeBtn = LocalizationResourceManager.Instance["Swipe_Remove"]?.ToString() ?? "Eemalda";

        var details  = $"{place.FullDescription}\n\n{addrLbl}: {place.Address}";
        var doRemove = await CurrentPage.DisplayAlertAsync(place.Name, details, removeBtn, closeBtn);

        if (doRemove)
            await RemoveFavoriteAsync(place);
    }
}
