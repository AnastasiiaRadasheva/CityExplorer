using CityExplorer.Resources.Strings;
using CityExplorer.ViewModels;

namespace CityExplorer.Views;

public partial class PlaceFormPage : ContentPage
{
    private readonly PlaceFormViewModel _vm;

    public PlaceFormPage(PlaceFormViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    private async void OnSaveClicked(object? sender, EventArgs e)
    {
        if (!_vm.Validate())
        {
            var msg = LocalizationResourceManager.Instance["Form_Required"]?.ToString()
                      ?? "Please fill in required fields (*)!";
            await DisplayAlertAsync(string.Empty, msg, "OK");
            return;
        }

        await _vm.SaveAsync();
        await Navigation.PopModalAsync();
    }

    private async void OnCancelClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
