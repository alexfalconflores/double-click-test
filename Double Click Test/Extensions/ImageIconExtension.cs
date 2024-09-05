using Microsoft.UI.Xaml.Controls;
using System;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media.Imaging;

namespace Double_Click_Test.Extensions;

[MarkupExtensionReturnType(ReturnType = typeof(ImageIcon))]
public sealed partial class ImageIconExtension : MarkupExtension
{
    public Uri? Source { get; set; }
    protected override object ProvideValue()
    {
        return new ImageIcon
        {
            Source = new BitmapImage(Source)
        };
    }
}
