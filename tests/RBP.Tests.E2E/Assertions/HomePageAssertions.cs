using AwesomeAssertions;
using RBP.Data.DTO.Room;
using RestfulBooker.Data.DTO;

namespace RBP.Tests.E2E.Assertions;

public static class HomePageAssertions
{
    public static void ShouldMatchApiRooms(this IReadOnlyCollection<RoomCardDto> uiRooms, IReadOnlyCollection<RoomDto> apiRooms)
    {
        uiRooms.Should().HaveCount(apiRooms.Count);

        foreach (RoomDto apiRoom in apiRooms)
        {
            RoomCardDto uiRoom =
                uiRooms.Single(x => x.Id == apiRoom.RoomId);

            uiRoom.Type.Should().Be(apiRoom.Type);

            uiRoom.Description.Should().Be(apiRoom.Description);

            uiRoom.Image.Should().Be(apiRoom.Image);

            uiRoom.Price.Should().Be(apiRoom.RoomPrice);

            uiRoom.Features.Should()
                .BeEquivalentTo(apiRoom.Features);
        }
    }
}
