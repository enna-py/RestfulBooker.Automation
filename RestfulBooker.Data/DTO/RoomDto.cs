namespace RestfulBooker.Data.DTO;

public sealed class RoomDto
{
    public int RoomId { get; init; }

    public string RoomName { get; init; } = string.Empty;

    public string Type { get; init; } = string.Empty;

    public bool Accessible { get; init; }

    public string Image { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public IReadOnlyCollection<string> Features { get; init; } = [];

    public int RoomPrice { get; init; }
}