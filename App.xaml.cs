using System.Globalization;
using CityExplorer.Resources.Strings;

namespace CityExplorer;

public partial class App : Application
{
    private readonly MainTabbedPage _tabbedPage;

    public App(MainTabbedPage tabbedPage)
    {
        InitializeComponent();

        var savedLang = Preferences.Default.Get("app_language", "et");
        LocalizationResourceManager.Instance.SetCulture(new CultureInfo(savedLang));

        _tabbedPage = tabbedPage;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(_tabbedPage);
    }
}
