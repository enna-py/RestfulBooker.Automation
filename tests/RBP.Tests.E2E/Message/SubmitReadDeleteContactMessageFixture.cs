using AwesomeAssertions;
using RBP.Business.Ui.Pages;
using RBP.Business.Ui.Pages.Admin;
using RBP.Business.Ui.Steps;
using RBP.Data.Builders.Message;
using RBP.Data.DTO.Message;
using RBP.Tests.E2E.Base;
using RestfulBooker.Core.Constants;

namespace RBP.Tests.E2E.Message;

public class SubmitReadDeleteContactMessageFixture : BaseFixture
{
    [Test]
    [Category(TestType.E2E)]
    [Category(TestType.Regression)]
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

        await contactMessageSteps.ShouldHaveSubmittedAsync();

        IReadOnlyCollection<MessageListItemDto> messagesAfterSubmit =
            await MessageApiClient.GetMessagesAsync();

        MessageListItemDto submittedMessage = messagesAfterSubmit.First(m =>
            m.Name == request.Name &&
            m.Subject == request.Subject);

        MessageDto apiMessage = await MessageApiClient.GetMessageAsync(submittedMessage.Id);

        ShouldMatch(apiMessage, request);

        AdminLoginPage loginPage = await CreatePage<AdminLoginPage>().OpenAsync();

        AuthenticationSteps authenticationSteps = new(loginPage);

        await authenticationSteps.LoginAsAdminAsync();

        AdminMessagesPage messagesPage = await CreatePage<AdminMessagesPage>().OpenAsync();

        AdminMessagesSteps adminMessagesSteps = new(messagesPage);

        await adminMessagesSteps.ShouldMatchMessageDetailsAsync(subject, request);

        await adminMessagesSteps.DeleteMessageAsync(subject);

        await adminMessagesSteps.ShouldNotContainMessageAsync(subject);

        IReadOnlyCollection<MessageListItemDto> messagesAfterDelete =
            await MessageApiClient.GetMessagesAsync();

        messagesAfterDelete.Should().NotContain(m => m.Id == submittedMessage.Id);
    }

    // API-level integrity check: the message the API actually stored matches what was
    // submitted. Not a UI outcome, so it doesn't belong on a Page Step - kept as a private
    // helper here rather than a shared Assertions class, since this is its only consumer.
    private static void ShouldMatch(MessageDto actual, ContactMessageRequest expected)
    {
        actual.Name.Should().Be(expected.Name);

        actual.Email.Should().Be(expected.Email);

        actual.Phone.Should().Be(expected.Phone);

        actual.Subject.Should().Be(expected.Subject);

        actual.Description.Should().Be(expected.Description);
    }
}
