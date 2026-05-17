using CityExplorer.ViewModels;

namespace CityExplorer.Views;

public partial class ExplorePage : ContentPage
{
    private readonly ExploreViewModel _vm;

    public ExplorePage(ExploreViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.StartAutoScroll();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _vm.StopAutoScroll();
    }
}
