

using System.Windows.Input;
using CityExplorer.Models;
using CityExplorer.Services;

namespace CityExplorer.ViewModels;

public class PlaceFormViewModel : BaseViewModel
{
    private readonly DatabaseService _db;

    private readonly Place? _existing;

    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    private string _description = string.Empty;
    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    private string _fullDescription = string.Empty;
    public string FullDescription
    {
        get => _fullDescription;
        set => SetProperty(ref _fullDescription, value);
    }

    private string _address = string.Empty;
    public string Address
    {
        get => _address;
        set => SetProperty(ref _address, value);
    }

    private string _selectedEmoji = "🏰";
    public string SelectedEmoji
    {
        get => _selectedEmoji;
        set
        {
            if (SetProperty(ref _selectedEmoji, value))
            {

                OnPropertyChanged(nameof(HistoryBtnColor));
                OnPropertyChanged(nameof(ParksBtnColor));
                OnPropertyChanged(nameof(FoodBtnColor));
            }
        }
    }

    private string _imageUrl = string.Empty;
    public string ImageUrl
    {
        get => _imageUrl;
        set
        {
            if (SetProperty(ref _imageUrl, value))
                OnPropertyChanged(nameof(HasImage)); 
        }
    }

    public bool HasImage => !string.IsNullOrEmpty(_imageUrl);

    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    private static readonly Color Inactive = Color.FromArgb("#2e1406");

    public Color HistoryBtnColor => _selectedEmoji == "🏰" ? Color.FromArgb("#7c2d12") : Inactive;
    public Color ParksBtnColor   => _selectedEmoji == "🌳" ? Color.FromArgb("#14532d") : Inactive;
    public Color FoodBtnColor    => _selectedEmoji == "🍽️" ? Color.FromArgb("#1e3a8a") : Inactive;

    public bool IsEditMode => _existing != null;

    public string FormTitle => IsEditMode ? "Muuda kohta" : "Lisa uus koht";

    public ICommand SelectEmojiCommand { get; }

    public ICommand PickPhotoCommand   { get; }

    public PlaceFormViewModel(DatabaseService db, Place? existing = null)
    {
        _db       = db;
        _existing = existing;

        SelectEmojiCommand = new Command<string>(emoji => SelectedEmoji = emoji);
        PickPhotoCommand   = new Command(async () => await PickPhotoAsync());

        if (existing != null)
        {
            Name            = existing.Name;
            Description     = existing.Description;
            FullDescription = existing.FullDescription;
            Address         = existing.Address;
            SelectedEmoji   = existing.CategoryEmoji;
            ImageUrl        = existing.ImageUrl;
        }
    }

    private async Task PickPhotoAsync()
    {
        try
        {
            var options = new PickOptions
            {
                PickerTitle = "Vali foto",
                FileTypes   = FilePickerFileType.Images
            };

            var result = await FilePicker.Default.PickAsync(options);
            if (result == null) return; // Пользователь отменил выбор.

            IsBusy = true;

            var photosDir = Path.Combine(FileSystem.AppDataDirectory, "photos");
            Directory.CreateDirectory(photosDir);

            var ext      = Path.GetExtension(result.FileName).ToLowerInvariant();
            var fileName = $"place_{Guid.NewGuid():N}{ext}"; // Уникальное имя файла.
            var destPath = Path.Combine(photosDir, fileName);

            using var src = await result.OpenReadAsync();
            using var dst = File.Create(destPath);
            await src.CopyToAsync(dst);

            ImageUrl = destPath;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Photo pick error: {ex.Message}");
        }
        finally
        {
            IsBusy = false; // Скрываем спиннер в любом случае.
        }
    }

    public bool Validate() =>
        !string.IsNullOrWhiteSpace(Name) &&
        !string.IsNullOrWhiteSpace(Description);

    public async Task SaveAsync()
    {
    
        var place = _existing ?? new Place();

        place.Name            = Name.Trim();
        place.Description     = Description.Trim();
        place.FullDescription = FullDescription.Trim();
        place.Address         = Address.Trim();
        place.CategoryEmoji   = SelectedEmoji;
        place.ImageUrl        = ImageUrl.Trim();
         
        await _db.SavePlaceAsync(place);
    }
}
