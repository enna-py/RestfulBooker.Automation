using Microsoft.Playwright;
using RBP.Business.Ui.Components;
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

    public async Task<MessageDetailComponent> OpenMessageAsync(string identifier)
    {
        LoggerManager.Logger.Information(
            "Opening message '{Identifier}'",
            identifier);

        await MessageRow(identifier).ClickAsync();

        return new MessageDetailComponent(Page);
    }

    public async Task<AdminMessagesPage> DeleteMessageAsync(string identifier)
    {
        LoggerManager.Logger.Information(
            "Deleting message '{Identifier}'",
            identifier);

        var deleteResponse = Page.WaitForResponseAsync(r =>
            r.Url.Contains("/api/message/") &&
            r.Request.Method == "DELETE");

        await MessageRow(identifier).Locator(".roomDelete").ClickAsync();

        await deleteResponse;

        return this;
    }
}
