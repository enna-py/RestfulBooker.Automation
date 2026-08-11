using Microsoft.Playwright;
using RestfulBooker.Core.Configuration;
using RestfulBooker.Core.Logging;

namespace RBP.Business.Ui.Pages.Admin;

public sealed class AdminReportPage : BasePage
{
    private ILocator Calendar =>
        Page.Locator(".rbc-calendar");

    public ILocator BookingEvent(string guestFullName, int roomNumber) =>
        Calendar.Locator(".rbc-event-content")
            .Filter(new() { HasText = $"{guestFullName} - Room: {roomNumber}" });

    public AdminReportPage(IPage page)
        : base(page)
    {
    }

    public async Task<AdminReportPage> OpenAsync()
    {
        LoggerManager.Logger.Information(
            "Opening Admin report page");

        await Page.GotoAsync(
            $"{ConfigurationService.Current.Ui.BaseUrl}/admin/report");

        return this;
    }
}
