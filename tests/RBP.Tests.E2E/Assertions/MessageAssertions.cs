using AwesomeAssertions;
using RBP.Data.DTO.Message;

namespace RBP.Tests.E2E.Assertions;
// todo remove duplication
public static class MessageAssertions
{
    public static void ShouldMatch(this ContactMessageRequest actual, ContactMessageRequest expected)
    {
        actual.Name.Should().Be(expected.Name);

        actual.Email.Should().Be(expected.Email);

        actual.Phone.Should().Be(expected.Phone);

        actual.Subject.Should().Be(expected.Subject);

        actual.Description.Should().Be(expected.Description);
    }
}
