using System;
using System.Threading.Tasks;

using Double_Click_Test.Services;

using Windows.ApplicationModel.Activation;

namespace Double_Click_Test.Activation;

internal class DefaultActivationHandler(Type navElement) : ActivationHandler<IActivatedEventArgs>
{
    private readonly Type _navElement = navElement;

    protected override async Task HandleInternalAsync(IActivatedEventArgs args)
    {
        // When the navigation stack isn't restored, navigate to the first page and configure
        // the new page by passing required information in the navigation parameter
        object arguments = null;
        if (args is LaunchActivatedEventArgs launchArgs)
        {
            arguments = launchArgs.Arguments;
        }

        NavigationService.Navigate(_navElement, arguments);
        await Task.CompletedTask;
    }

    protected override bool CanHandleInternal(IActivatedEventArgs args)
    {
        // None of the ActivationHandlers has handled the app activation
        return NavigationService.Frame.Content == null && _navElement != null;
    }
}
