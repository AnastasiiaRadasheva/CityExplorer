
using System.Collections.ObjectModel;
using System.Windows.Input;
using CityExplorer.Models;
using CityExplorer.Resources.Strings;
using CityExplorer.Services;

namespace CityExplorer.ViewModels;

public class ExploreViewModel : BaseViewModel
{
   
    private readonly DatabaseService _db;
    private CancellationTokenSource? _scrollCts;

    public ObservableCollection<Place> Places { get; } = new();

    public List<Category> Categories { get; } = new()
    {
        new Category { Emoji = "🏰", TitleKey = "Category_History", Color = "#7c2d12" },
        new Category { Emoji = "🌳", TitleKey = "Category_Parks",   Color = "#14532d" },
        new Category { Emoji = "🍽️", TitleKey = "Category_Food",    Color = "#1e3a8a" },
    };

    private int _currentPosition;
    public int CurrentPosition
    {
        get => _currentPosition;
        set => SetProperty(ref _currentPosition, value);
    }
    public ICommand SelectCategoryCommand { get; }
    public ICommand AddToFavoritesCommand { get; }
    public ICommand ShowDetailsCommand    { get; }

    public ExploreViewModel(DatabaseService db)
    {
        _db = db;

        SelectCategoryCommand = new Command<Category>(async cat => await LoadPlacesAsync(cat.Emoji));

        AddToFavoritesCommand = new Command<Place>(async p => await AddToFavoritesAsync(p));

        ShowDetailsCommand = new Command<Place>(async p => await ShowDetailsAsync(p));

        _ = LoadPlacesAsync("🏰");
    }

    private static Page CurrentPage => Application.Current!.Windows[0].Page!;

    public void StartAutoScroll()
    {
        _scrollCts?.Cancel(); 
        _scrollCts = new CancellationTokenSource();
        _ = AutoScrollLoopAsync(_scrollCts.Token);
    }

    public void StopAutoScroll() => _scrollCts?.Cancel();

    private async Task AutoScrollLoopAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            try { await Task.Delay(6000, ct); }
            catch (TaskCanceledException) { break; }

            if (ct.IsCancellationRequested || Places.Count == 0) break;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (!ct.IsCancellationRequested && Places.Count > 0)
                    CurrentPosition = (CurrentPosition + 1) % Places.Count; 
            });
        }
    }

    public async Task LoadPlacesAsync(string emoji)
    {
        Places.Clear();
        CurrentPosition = 0;

        var list = await _db.GetPlacesByCategoryAsync(emoji);

        foreach (var p in list)
        {
            p.ApplyCardColor();
            Places.Add(p); 
        }
    }

    private async Task AddToFavoritesAsync(Place place)
    {
        if (place.IsFavorite)
        {
            var alreadyTitle = LocalizationResourceManager.Instance["Alert_Already_Title"]?.ToString() ?? "";
            var alreadyMsg   = LocalizationResourceManager.Instance["Alert_Already_Msg"]?.ToString() ?? "";
            var ok           = LocalizationResourceManager.Instance["Alert_OK"]?.ToString() ?? "OK";
            await CurrentPage.DisplayAlertAsync(alreadyTitle, $"«{place.Name}» {alreadyMsg}", ok);
            return;
        }

        place.IsFavorite = true;

        await _db.SavePlaceAsync(place);

        var title = LocalizationResourceManager.Instance["Alert_Added_Title"]?.ToString() ?? "";
        var msg   = LocalizationResourceManager.Instance["Alert_Added_Msg"]?.ToString() ?? "";
        var okBtn = LocalizationResourceManager.Instance["Alert_OK"]?.ToString() ?? "OK";
        await CurrentPage.DisplayAlertAsync(title, $"«{place.Name}» {msg}", okBtn);
    }

    private async Task ShowDetailsAsync(Place place)
    {
        StopAutoScroll();

        var addBtn   = LocalizationResourceManager.Instance["Btn_AddFavorites"]?.ToString() ?? "⭐";
        var closeBtn = LocalizationResourceManager.Instance["Alert_Close"]?.ToString() ?? "Sulge";
        var addrLbl  = LocalizationResourceManager.Instance["Alert_Address"]?.ToString() ?? "📍";

        var details = $"{place.FullDescription}\n\n{addrLbl}: {place.Address}";
        var result  = await CurrentPage.DisplayAlertAsync(place.Name, details, addBtn, closeBtn);

        if (result)
            await AddToFavoritesAsync(place);

        StartAutoScroll();
    }
}
