using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Double_Click_Test.Activation;

using Windows.ApplicationModel.Activation;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Double_Click_Test.Services;

// For more information on understanding and extending activation flow see
// https://github.com/microsoft/TemplateStudio/blob/main/docs/UWP/activation.md
internal class ActivationService(App app, Type defaultNavItem, Lazy<UIElement> shell = null)
{
    private object _lastActivationArgs;

    public static readonly KeyboardAccelerator AltLeftKeyboardAccelerator = BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu);

    public static readonly KeyboardAccelerator BackKeyboardAccelerator = BuildKeyboardAccelerator(VirtualKey.GoBack);

    public async Task ActivateAsync(object activationArgs)
    {
        if (IsInteractive(activationArgs))
        {
            // Initialize services that you need before app activation
            // take into account that the splash screen is shown while this code runs.
            await InitializeAsync();

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (Window.Current.Content == null)
            {
                // Create a Shell or Frame to act as the navigation context
                Window.Current.Content = shell?.Value ?? new Frame();
                NavigationService.NavigationFailed += (sender, e) =>
                {
                    throw e.Exception;
                };
            }
        }

        // Depending on activationArgs one of ActivationHandlers or DefaultActivationHandler
        // will navigate to the first page
        await HandleActivationAsync(activationArgs);
        _lastActivationArgs = activationArgs;

        if (IsInteractive(activationArgs))
        {
            // Ensure the current window is active
            Window.Current.Activate();

            // Tasks after activation
            await StartupAsync();
        }
    }

    private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = null)
    {
        var keyboardAccelerator = new KeyboardAccelerator() { Key = key };
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

    private async Task InitializeAsync()
    {
        await ThemeSelectorService.InitializeAsync().ConfigureAwait(false);
    }

    private async Task HandleActivationAsync(object activationArgs)
    {
        var activationHandler = GetActivationHandlers()
                                            .FirstOrDefault(h => h.CanHandle(activationArgs));

        if (activationHandler != null)
        {
            await activationHandler.HandleAsync(activationArgs);
        }

        if (IsInteractive(activationArgs))
        {
            var defaultHandler = new DefaultActivationHandler(defaultNavItem);
            if (defaultHandler.CanHandle(activationArgs))
            {
                await defaultHandler.HandleAsync(activationArgs);
            }
        }
    }

    private async Task StartupAsync()
    {
        await ThemeSelectorService.SetRequestedThemeAsync();
        await FirstRunDisplayService.ShowIfAppropriateAsync();
        await WhatsNewDisplayService.ShowIfAppropriateAsync();
    }

    private IEnumerable<ActivationHandler> GetActivationHandlers()
    {
        yield break;
    }

    private bool IsInteractive(object args)
    {
        return args is IActivatedEventArgs;
    }
}
