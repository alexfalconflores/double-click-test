using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace Double_Click_Test;
public sealed partial class ShellPage : Page
{
    public ShellViewModel ViewModel { get; } = new();
    public ShellPage()
    {
        this.InitializeComponent();
        ViewModel.Initialize(ShellFrame, KeyboardAccelerators);
        CoreWindow.GetForCurrentThread().PointerPressed += ViewModel.PointerPressed;
    }
}
