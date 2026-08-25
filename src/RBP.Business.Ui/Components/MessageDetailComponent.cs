using Microsoft.Playwright;
using RBP.Business.Ui.Pages.Admin;
using RBP.Data.DTO.Message;
using RestfulBooker.Core.Logging;

namespace RBP.Business.Ui.Components;

public sealed class MessageDetailComponent
{
    private readonly IPage _page;

    public MessageDetailComponent(IPage page)
    {
        _page = page;
    }

    private ILocator Modal =>
        _page.Locator("[data-testid='message']");

    private ILocator FromAndPhoneRow =>
        Modal.Locator(".form-row").Nth(0);

    private ILocator EmailRow =>
        Modal.Locator(".form-row").Nth(1);

    private ILocator SubjectRow =>
        Modal.Locator(".form-row").Nth(2);

    private ILocator DescriptionRow =>
        Modal.Locator(".form-row").Nth(3);

    private ILocator CloseButton =>
        Modal.Locator(".form-row").Nth(4).GetByRole(AriaRole.Button);

    public async Task<ContactMessageRequest> GetDetailsAsync()
    {
        LoggerManager.Logger.Information(
            "Reading message detail");

        string fromText = await FromAndPhoneRow.Locator(".col-10 p").InnerTextAsync();

        string phoneText = await FromAndPhoneRow.Locator(".col-2 p").InnerTextAsync();

        string emailText = await EmailRow.Locator("p").InnerTextAsync();

        string subjectText = await SubjectRow.Locator("p").InnerTextAsync();

        string descriptionText = await DescriptionRow.Locator("p").InnerTextAsync();

        return new ContactMessageRequest
        {
            Name = fromText.Replace("From: ", string.Empty),
            Phone = phoneText.Replace("Phone: ", string.Empty),
            Email = emailText.Replace("Email: ", string.Empty),
            Subject = subjectText,
            Description = descriptionText
        };
    }

    public async Task<AdminMessagesPage> CloseAsync()
    {
        LoggerManager.Logger.Information(
            "Closing message detail");

        await CloseButton.ClickAsync();

        return new AdminMessagesPage(_page);
    }
}
