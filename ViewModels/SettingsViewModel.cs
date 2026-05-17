using System.Globalization;
using System.Windows.Input;
using CityExplorer.Resources.Strings;

namespace CityExplorer.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    private string _selectedLanguage;

    public string SelectedLanguage
    {
        get => _selectedLanguage;
        private set
        {
            if (SetProperty(ref _selectedLanguage, value))
            {
                OnPropertyChanged(nameof(IsEtSelected));
                OnPropertyChanged(nameof(IsEnSelected));
                OnPropertyChanged(nameof(IsRuSelected));
                OnPropertyChanged(nameof(EtBtnColor));
                OnPropertyChanged(nameof(EnBtnColor));
                OnPropertyChanged(nameof(RuBtnColor));
                OnPropertyChanged(nameof(EtTextColor));
                OnPropertyChanged(nameof(EnTextColor));
                OnPropertyChanged(nameof(RuTextColor));
            }
        }
    }

    public bool IsEtSelected => SelectedLanguage == "et";
    public bool IsEnSelected => SelectedLanguage == "en";
    public bool IsRuSelected => SelectedLanguage == "ru";

    private static readonly Color ActiveBg    = Color.FromArgb("#ea580c");
    private static readonly Color InactiveBg  = Color.FromArgb("#ffffff");
    private static readonly Color ActiveText  = Colors.White;
    private static readonly Color InactiveText = Color.FromArgb("#1c1917");

    public Color EtBtnColor  => IsEtSelected ? ActiveBg   : InactiveBg;
    public Color EnBtnColor  => IsEnSelected ? ActiveBg   : InactiveBg;
    public Color RuBtnColor  => IsRuSelected ? ActiveBg   : InactiveBg;
    public Color EtTextColor => IsEtSelected ? ActiveText : InactiveText;
    public Color EnTextColor => IsEnSelected ? ActiveText : InactiveText;
    public Color RuTextColor => IsRuSelected ? ActiveText : InactiveText;

    public ICommand SetEstonianCommand { get; }
    public ICommand SetEnglishCommand  { get; }
    public ICommand SetRussianCommand  { get; }

    public SettingsViewModel()
    {
        _selectedLanguage = Preferences.Default.Get("app_language", "et");

        SetEstonianCommand = new Command(() => SetLanguage("et"));
        SetEnglishCommand  = new Command(() => SetLanguage("en"));
        SetRussianCommand  = new Command(() => SetLanguage("ru"));
    }

    private void SetLanguage(string code)
    {
        if (SelectedLanguage == code) return;

        Preferences.Default.Set("app_language", code);
        LocalizationResourceManager.Instance.SetCulture(new CultureInfo(code));
        SelectedLanguage = code;
    }
}
