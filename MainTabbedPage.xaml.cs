using CityExplorer.Views;

namespace CityExplorer;

public partial class MainTabbedPage : TabbedPage
{
    public MainTabbedPage(ExplorePage explorePage, FavoritesPage favoritesPage,
                          SettingsPage settingsPage, AdminPage adminPage)
    {
        InitializeComponent();
        Children.Add(explorePage);
        Children.Add(favoritesPage);
        Children.Add(settingsPage);
        Children.Add(adminPage);
    }
}
