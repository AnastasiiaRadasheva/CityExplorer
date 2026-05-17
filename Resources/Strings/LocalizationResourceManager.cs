using System.ComponentModel;
using System.Globalization;

namespace CityExplorer.Resources.Strings;

public class LocalizationResourceManager : INotifyPropertyChanged
{
    private LocalizationResourceManager() { }

    public static LocalizationResourceManager Instance { get; } = new();

    public object? this[string key] =>
        AppResources.ResourceManager.GetString(key, AppResources.Culture) ?? key;

    public void SetCulture(CultureInfo culture)
    {
        AppResources.Culture = culture;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
