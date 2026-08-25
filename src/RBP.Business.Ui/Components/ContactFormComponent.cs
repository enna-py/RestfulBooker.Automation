using Microsoft.Playwright;
using RBP.Data.DTO.Message;
using RestfulBooker.Core.Logging;

namespace RBP.Business.Ui.Components;

public sealed class ContactFormComponent
{
    private readonly IPage _page;

    public ContactFormComponent(IPage page)
    {
        _page = page;
    }

    private ILocator Name =>
        _page.Locator("#name");

    private ILocator Email =>
        _page.Locator("#email");

    private ILocator Phone =>
        _page.Locator("#phone");

    private ILocator Subject =>
        _page.Locator("#subject");

    private ILocator Description =>
        _page.Locator("#description");

    private ILocator SubmitButton =>
        _page.GetByRole(AriaRole.Button, new() { Name = "Submit" });

    public async Task<ContactFormComponent> FillAsync(ContactMessageRequest request)
    {
        LoggerManager.Logger.Information(
            "Filling contact form for '{Name}', subject '{Subject}'",
            request.Name,
            request.Subject);

        await Name.FillAsync(request.Name);

        await Email.FillAsync(request.Email);

        await Phone.FillAsync(request.Phone);

        await Subject.FillAsync(request.Subject);

        await Description.FillAsync(request.Description);

        return this;
    }

    public async Task SubmitAsync()
    {
        LoggerManager.Logger.Information(
            "Submitting contact message");

        var submitResponse = _page.WaitForResponseAsync(r =>
            r.Url.Contains("/api/message") &&
            r.Request.Method == "POST");

        await SubmitButton.ClickAsync();

        await submitResponse;
    }
}
