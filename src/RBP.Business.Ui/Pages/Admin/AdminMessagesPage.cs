using Microsoft.Playwright;
using RestfulBooker.Core.Configuration;
using RestfulBooker.Core.Logging;

namespace RBP.Business.Ui.Pages.Admin;

public sealed class AdminMessagesPage : BasePage
{
    private ILocator Messages =>
        Page.Locator(".messages");

    public ILocator MessageRow(string guestFullName) =>
        Messages.Locator(".row")
            .Filter(new() { HasText = guestFullName });

    public AdminMessagesPage(IPage page)
        : base(page)
    {
    }

    public async Task<AdminMessagesPage> OpenAsync()
    {
        LoggerManager.Logger.Information(
            "Opening Admin messages page");

        await Page.GotoAsync(
            $"{ConfigurationService.Current.Ui.BaseUrl}/admin/message");

        return this;
    }
}
