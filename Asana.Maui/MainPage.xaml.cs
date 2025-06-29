using Asana.Maui.ViewModel;
namespace Asana.Maui;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainPageViewModel();
    }
}
