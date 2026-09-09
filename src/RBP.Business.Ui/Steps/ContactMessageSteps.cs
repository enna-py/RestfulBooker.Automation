using RBP.Business.Ui.Pages;
using RBP.Data.DTO.Message;
using RestfulBooker.Core.Logging;
using static Microsoft.Playwright.Assertions;

namespace RBP.Business.Ui.Steps;

public sealed class ContactMessageSteps
{
    private readonly HomePage _homePage;

    public ContactMessageSteps(HomePage homePage)
    {
        _homePage = homePage;
    }

    public async Task SubmitMessageAsync(ContactMessageRequest request)
    {
        LoggerManager.Logger.Information(
            "User submits a contact message '{Subject}'",
            request.Subject);

        await _homePage.ContactForm.FillAsync(request);

        await _homePage.ContactForm.SubmitAsync();
    }

    public async Task ShouldHaveSubmittedAsync()
    {
        await Expect(_homePage.ContactConfirmationMessage).ToBeVisibleAsync();
    }
}
