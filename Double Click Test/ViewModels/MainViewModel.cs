using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Double_Click_Test.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private Stopwatch stopwatch;
    private long prevClickTimeTicks;

    [ObservableProperty]
    private string prevDiff = string.Empty;
    [ObservableProperty]
    private int clickCount = 0;
    [ObservableProperty]
    private int doubleClickCount = 0;
    [ObservableProperty]
    private Brush clickFill = new SolidColorBrush(Colors.Orange);
    public MainViewModel()
    {
        //prevClickTimeTicks = DateTime.Now.Ticks;
        stopwatch = Stopwatch.StartNew();
    }

    [RelayCommand]
    private void MouseClick()
    {
        double diff = stopwatch.Elapsed.TotalSeconds;
        stopwatch.Restart();
        if(diff <= 0.08)
        {
            ClickFill = new SolidColorBrush(Colors.Red);
            DoubleClickCount++;
        }
        PrevDiff = diff + "\n" + PrevDiff;
        ClickCount++;
    }
    //private async Task MouseClick()
    //{
    //    await Task.Run(async () =>
    //    {
    //        long clickTimeTicks = DateTime.Now.Ticks;
    //        double diff = (clickTimeTicks - prevClickTimeTicks) / (double)TimeSpan.TicksPerMillisecond;
    //        if (diff <= 80)
    //        {
    //            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
    //            {
    //                ClickFill = new SolidColorBrush(Colors.Red);
    //                DoubleClickCount++;
    //            });
    //        }
    //        prevClickTimeTicks = clickTimeTicks;
    //        await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
    //        {

    //            PrevDiff = diff.ToString("F2") + "ms\n" + PrevDiff;
    //            ClickCount++;
    //        });
    //    });
    //}

    [RelayCommand]
    private void Reset()
    {
        ClickCount = 0;
        DoubleClickCount = 0;
        PrevDiff = string.Empty;
        ClickFill = new SolidColorBrush(Colors.Orange);
    }
}
