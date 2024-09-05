using System;

using Windows.UI.Xaml.Data;

namespace Double_Click_Test.Helpers;

public class EnumToBooleanConverter : IValueConverter
{
    public Type EnumType { get; set; }

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (parameter is not string enumString)
            throw new ArgumentException("parameter must be an Enum name!");
        if (!Enum.IsDefined(EnumType, value))
            throw new ArgumentException("value must be an Enum!");
        var enumValue = Enum.Parse(EnumType, enumString);
        return enumValue.Equals(value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (parameter is not string enumString)
        {
            throw new ArgumentException("parameter must be an Enum name!");
        }
        return Enum.Parse(EnumType, enumString);
    }
}
