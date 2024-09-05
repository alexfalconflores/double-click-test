using CommunityToolkit.Mvvm.ComponentModel;

namespace Double_Click_Test.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private int clickCount = 0;
    [ObservableProperty]
    private int doubleClickCount = 0;
    public MainViewModel()
    {
    }
}
