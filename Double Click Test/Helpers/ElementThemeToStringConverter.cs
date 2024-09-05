using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Double_Click_Test.Helpers;

public class ElementThemeToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language) =>
        value switch
        {
            ElementTheme.Default => "/SettingsPage/Settings_Theme_Default/Content".GetLocalized(),
            ElementTheme.Light => "/SettingsPage/Settings_Theme_Light/Content".GetLocalized(),
            ElementTheme.Dark => "/SettingsPage/Settings_Theme_Dark/Content".GetLocalized(),
            _ => null,
        };



    public object ConvertBack(object value, Type targetType, object parameter, string language) =>
        throw new NotImplementedException();

}
