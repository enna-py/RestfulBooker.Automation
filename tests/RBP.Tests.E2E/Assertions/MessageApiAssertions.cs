using AwesomeAssertions;
using RBP.Data.DTO.Message;

namespace RBP.Tests.E2E.Assertions;

public static class MessageApiAssertions
{
    public static void ShouldMatch(this MessageDto actual, ContactMessageRequest expected)
    {
        actual.Name.Should().Be(expected.Name);

        actual.Email.Should().Be(expected.Email);

        actual.Phone.Should().Be(expected.Phone);

        actual.Subject.Should().Be(expected.Subject);

        actual.Description.Should().Be(expected.Description);
    }

    public static void ShouldNotContainMessage(
        this IEnumerable<MessageListItemDto> messages,
        int messageId)
    {
        messages.Should().NotContain(m => m.Id == messageId);
    }
}
