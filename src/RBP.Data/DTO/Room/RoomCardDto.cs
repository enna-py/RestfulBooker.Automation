namespace RBP.Data.DTO.Room;

public sealed class RoomCardDto
{
    public int Id { get; init; } = 1;

    public string Type { get; init; } = "Single";

    public string Description { get; init; } = string.Empty;

    public decimal Price { get; init; }

    public string Image { get; init; } = "/images/room1.jpg";

    public IReadOnlyCollection<string> Features { get; init; }
        = [];
}
