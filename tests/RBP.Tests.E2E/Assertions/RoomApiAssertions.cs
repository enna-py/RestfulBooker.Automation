using AwesomeAssertions;
using RestfulBooker.Data.DTO;
using RestfulBooker.Data.DTO.Common;

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
}
