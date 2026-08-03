using Microsoft.Playwright;

namespace RBP.Business.Ui.Browser;

public sealed class BrowserSession : IAsyncDisposable
{
    private readonly IPlaywright _playwright;

    public IBrowser Browser { get; }

    internal BrowserSession(
        IPlaywright playwright,
        IBrowser browser)
    {
        _playwright = playwright;
        Browser = browser;
    }

    public async ValueTask DisposeAsync()
    {
        await Browser.CloseAsync();
        _playwright.Dispose();
    }
}
