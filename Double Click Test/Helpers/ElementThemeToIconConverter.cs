using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Double_Click_Test.Helpers;

public class ElementThemeToIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language) =>
    value switch
    {
        ElementTheme.Default => "\uE7F4",
        ElementTheme.Light => "\uE706",
        ElementTheme.Dark => "\uE708",
        _ => null,
    };

    public object ConvertBack(object value, Type targetType, object parameter, string language) =>
        throw new NotImplementedException();
}
