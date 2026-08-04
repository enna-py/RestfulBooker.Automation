using Microsoft.Playwright;
using RestfulBooker.Core.Configuration;

namespace RBP.Business.Ui.Browser;

public static class BrowserFactory
{
    public static async Task<BrowserSession> CreateAsync(UiSettings settings)
    {
        IPlaywright playwright = await Playwright.CreateAsync();

        IBrowserType browserType =
            settings.Browser.ToLowerInvariant() switch
            {
                "chromium" => playwright.Chromium,
                "firefox" => playwright.Firefox,
                "webkit" => playwright.Webkit,

                _ => throw new ArgumentException(
                    $"Unsupported browser: {settings.Browser}")
            };

        IBrowser browser =
            await browserType.LaunchAsync(
                new BrowserTypeLaunchOptions
                {
                    Headless = settings.Headless
                });

        return new BrowserSession(
            playwright,
            browser);
    }
}