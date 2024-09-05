using System;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Data;

namespace Double_Click_Test.Helpers;

public class CornerRadiusToDoubleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if(value is Windows.UI.Xaml.CornerRadius cornerRadius)
        {
            return cornerRadius.TopLeft;
        }
        return 0.0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
