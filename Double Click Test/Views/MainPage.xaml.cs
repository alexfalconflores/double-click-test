using Double_Click_Test.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Double_Click_Test.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel { get; } = new MainViewModel();

    public MainPage()
    {
        InitializeComponent();
    }
}
