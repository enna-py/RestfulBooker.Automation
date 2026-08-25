using AwesomeAssertions;
using RestfulBooker.Data.DTO;
using RestfulBooker.Data.DTO.Common;
using RestfulBooker.Data.DTO.Room;

namespace RBP.Tests.E2E.Assertions;

public static class RoomApiAssertions
{
    public static void ShouldIndicateSuccessfulDeletion(
        this ApiResponse<object> response)
    {
        response.IsSuccessful.Should().BeTrue();
    }

    public static void ShouldNotContainRoom(
        this IEnumerable<RoomDto> rooms,
        int roomId)
    {
        rooms.Should().NotContain(r => r.RoomId == roomId);
    }

    public static void ShouldMatch(this RoomDto actual, RoomApiRequest expected)
    {
        actual.Type.Should().Be(expected.Type);
        actual.Accessible.Should().Be(expected.Accessible);
        actual.RoomPrice.Should().Be(expected.RoomPrice);
        actual.Features.Should().BeEquivalentTo(expected.Features);
    }
}
