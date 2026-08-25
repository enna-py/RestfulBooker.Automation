using RBP.Business.Ui.Pages;
using RBP.Data.DTO.Message;

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
        await _homePage.ContactForm.FillAsync(request);

        await _homePage.ContactForm.SubmitAsync();
    }
}
