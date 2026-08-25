using RBP.Business.Ui.Components;
using RBP.Business.Ui.Pages;
using RBP.Business.Ui.Pages.Admin;
using RBP.Business.Ui.Steps;
using RBP.Data.Builders.Message;
using RBP.Data.DTO.Message;
using RBP.Tests.E2E.Assertions;
using RBP.Tests.E2E.Base;

namespace RBP.Tests.E2E.Message;

public class SubmitReadDeleteContactMessageFixture : BaseFixture
{
    [Test]
    [Category("E2E")]
    [Category("Regression")]
    [Property("JiraKey", "RBP-15")]
    public async Task Contact_Message_Should_Be_Submitted_Read_And_Deleted()
    {
        string subject = $"TC09 automated enquiry {DateTime.UtcNow:HHmmssfff}";

        ContactMessageRequest request = new ContactMessageRequestBuilder()
            .WithName("TC09 Contact Tester")
            .WithEmail("tc09@test.com")
            .WithPhone("01234567890")
            .WithSubject(subject)
            .WithDescription("Automated TC09 message body with enough length to satisfy validation.")
            .Build();

        await AuthApiClient.LoginAsync();

        HomePage homePage = await CreatePage<HomePage>().OpenAsync();

        ContactMessageSteps contactMessageSteps = new(homePage);

        await contactMessageSteps.SubmitMessageAsync(request);

        await homePage.ShouldHaveSubmittedContactMessage();

        IReadOnlyCollection<MessageListItemDto> messagesAfterSubmit =
            await MessageApiClient.GetMessagesAsync();

        MessageListItemDto submittedMessage = messagesAfterSubmit.First(m =>
            m.Name == request.Name &&
            m.Subject == request.Subject);

        MessageDto apiMessage = await MessageApiClient.GetMessageAsync(submittedMessage.Id);

        apiMessage.ShouldMatch(request);

        AdminLoginPage loginPage = await CreatePage<AdminLoginPage>().OpenAsync();

        AuthenticationSteps authenticationSteps = new(loginPage);

        await authenticationSteps.LoginAsAdminAsync();

        AdminMessagesPage messagesPage = await CreatePage<AdminMessagesPage>().OpenAsync();

        MessageDetailComponent messageDetail = await messagesPage.OpenMessageAsync(subject);

        ContactMessageRequest displayedMessage = await messageDetail.GetDetailsAsync();

        displayedMessage.ShouldMatch(request);

        messagesPage = await messageDetail.CloseAsync();

        messagesPage = await messagesPage.DeleteMessageAsync(subject);

        await messagesPage.ShouldNotContainMessage(subject);

        IReadOnlyCollection<MessageListItemDto> messagesAfterDelete =
            await MessageApiClient.GetMessagesAsync();

        messagesAfterDelete.ShouldNotContainMessage(submittedMessage.Id);
    }
}
