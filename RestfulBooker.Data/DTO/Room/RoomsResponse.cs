using RestfulBooker.Data.DTO;

namespace RestfulBooker.Data.DTO.Room
{
    public sealed class RoomsResponse
    {
        public IReadOnlyCollection<RoomDto> Rooms { get; init; } = [];
    }
}
