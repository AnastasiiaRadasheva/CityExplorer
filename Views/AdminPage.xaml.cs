using CityExplorer.Models;
using CityExplorer.Services;
using CityExplorer.ViewModels;

namespace CityExplorer.Views;

public partial class AdminPage : ContentPage
{
    private readonly AdminViewModel _vm;
    private readonly DatabaseService _db;

    public AdminPage(AdminViewModel vm, DatabaseService db)
    {
        InitializeComponent();
        _vm = vm;
        _db = db;
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadAsync();
    }

    private async void OnAddClicked(object? sender, EventArgs e)
    {
        var formVm = new PlaceFormViewModel(_db);
        var formPage = new PlaceFormPage(formVm);
        await Navigation.PushModalAsync(new NavigationPage(formPage)
        {
            BarBackgroundColor = Color.FromArgb("#ea580c"),
            BarTextColor = Colors.White
        });
    }

    private async void OnEditTapped(object? sender, TappedEventArgs e)
    {
        if (sender is not BindableObject bo) return;
        if (bo.BindingContext is not Place place) return;

        var formVm = new PlaceFormViewModel(_db, place);
        var formPage = new PlaceFormPage(formVm);
        await Navigation.PushModalAsync(new NavigationPage(formPage)
        {
            BarBackgroundColor = Color.FromArgb("#ea580c"),
            BarTextColor = Colors.White
        });
    }
}
