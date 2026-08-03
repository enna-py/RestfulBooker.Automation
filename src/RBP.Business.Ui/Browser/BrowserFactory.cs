using Microsoft.Playwright;
using RestfulBooker.Core.Configuration;

namespace RBP.Business.Ui.Browser;

public static class BrowserFactory
{
    public static async Task<BrowserSession> CreateAsync(UiSettings settings)
    {
        IPlaywright playwright = await Playwright.CreateAsync();

        IBrowser browser = await playwright.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions
            {
                Headless = settings.Headless
            });

        return new BrowserSession(
            playwright,
            browser);
    }
}
