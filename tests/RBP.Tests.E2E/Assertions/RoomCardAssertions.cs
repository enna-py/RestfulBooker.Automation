using AwesomeAssertions;
using RBP.Data.DTO.Room;

namespace RBP.Tests.E2E.Assertions;

public static class RoomCardAssertions
{
    public static void ShouldMatch(
        this RoomCardDto actual,
        RoomCardDto expected)
    {
        actual.Type.Should().Be(expected.Type);

        actual.Description.Should().Be(expected.Description);

        actual.Price.Should().Be(expected.Price);

        actual.Image.Should().Be(expected.Image);

        actual.Features.Should()
            .BeEquivalentTo(expected.Features);
    }
}
