using Microsoft.Playwright;
using RBP.Business.Ui.Pages.Admin;
using RBP.Data.DTO.Booking;
using RestfulBooker.Core.Logging;

namespace RBP.Business.Ui.Components;

public sealed class BookingEditComponent
{
    private readonly IPage _page;

    public BookingEditComponent(IPage page)
    {
        _page = page;
    }

    private ILocator Root =>
        _page.Locator(".detail")
            .Filter(new() { Has = _page.Locator("input[name='firstname']") });

    private ILocator FirstName =>
        Root.Locator("input[name='firstname']");

    private ILocator LastName =>
        Root.Locator("input[name='lastname']");

    private ILocator CheckIn =>
        Root.Locator(".dateWrapper input").Nth(0);

    private ILocator CheckOut =>
        Root.Locator(".dateWrapper input").Nth(1);

    private ILocator Confirm =>
        Root.Locator(".confirmBookingEdit");

    public async Task<BookingEditComponent> FillAsync(BookingRequest updatedValues)
    {
        LoggerManager.Logger.Information(
            "Filling booking edit form");

        await FirstName.FillAsync(updatedValues.Guest.FirstName);

        await LastName.FillAsync(updatedValues.Guest.LastName);

        await CheckIn.FillAsync(updatedValues.CheckIn.ToString("dd/MM/yyyy"));

        await CheckOut.FillAsync(updatedValues.CheckOut.ToString("dd/MM/yyyy"));

        return this;
    }

    public async Task<AdminRoomDetailsPage> SaveAsync()
    {
        LoggerManager.Logger.Information(
            "Saving booking changes");

        // Confirm doesn't navigate away - it PUTs the update in the background,
        // so wait for that request to complete before callers query the API,
        // otherwise a GET can race ahead of the save and read stale data.
        var saveResponse = _page.WaitForResponseAsync(r =>
            r.Url.Contains("/api/booking/") &&
            r.Request.Method == "PUT");

        await Confirm.ClickAsync();

        await saveResponse;

        return new AdminRoomDetailsPage(_page);
    }
}
