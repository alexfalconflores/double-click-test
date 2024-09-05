using Windows.ApplicationModel.Resources;

namespace Double_Click_Test.Helpers;

internal static class ResourceExtensions
{
    private static readonly ResourceLoader _resLoader = new();

    public static string GetLocalized(this string resourceKey) => _resLoader.GetString(resourceKey);
    
}
