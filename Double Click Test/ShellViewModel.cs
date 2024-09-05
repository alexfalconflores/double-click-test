using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Double_Click_Test.Services;
using System.Collections.Generic;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Double_Click_Test;

public partial class ShellViewModel : ObservableObject
{
    private readonly KeyboardAccelerator _altLeftKeyboardAccelerator = BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu);
    private IList<KeyboardAccelerator> _keyboardAccelerators;

    [ObservableProperty]
    private bool isBackEnabled;
    [ObservableProperty]
    private bool isBackButtonVisible;

    public void Initialize(Frame shellFrame, IList<KeyboardAccelerator> keyboardAccelerators)
    {
        NavigationService.Frame = shellFrame;
        _keyboardAccelerators = keyboardAccelerators;
        NavigationService.Navigated += Frame_Navigated;
        NavigationService.NavigationFailed += Frame_NavigationFailed;
    }

    [RelayCommand]
    private void OnLoaded() => _keyboardAccelerators.Add(_altLeftKeyboardAccelerator);
    [RelayCommand]
    private void OnMainPage() => NavigationService.Navigate(typeof(Views.MainPage), null, new SuppressNavigationTransitionInfo());
    [RelayCommand]
    private void OnSettingsPage() => NavigationService.Navigate(typeof(Views.SettingsPage), null, new SuppressNavigationTransitionInfo());
    [RelayCommand]
    private void OnBackPage() => NavigationService.GoBack();

    private void Frame_Navigated(object sender, NavigationEventArgs e)
    {
        IsBackEnabled = NavigationService.CanGoBack;
        IsBackButtonVisible = IsBackEnabled;
    }

    private void Frame_NavigationFailed(object sender, Windows.UI.Xaml.Navigation.NavigationFailedEventArgs e) => throw e.Exception;

    private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = null)
    {
        KeyboardAccelerator keyboardAccelerator = new() { Key = key };
        if (modifiers.HasValue)
        {
            keyboardAccelerator.Modifiers = modifiers.Value;
        }

        keyboardAccelerator.Invoked += OnKeyboardAcceleratorInvoked;
        return keyboardAccelerator;
    }

    private static void OnKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        var result = NavigationService.GoBack();
        args.Handled = result;
    }

    public void PointerPressed(CoreWindow sender, PointerEventArgs args)
    {
        if (args.CurrentPoint.Properties.IsXButton1Pressed && NavigationService.CanGoBack)
        {
            var result = NavigationService.GoBack();
            args.Handled = result;
        }
    }
}
