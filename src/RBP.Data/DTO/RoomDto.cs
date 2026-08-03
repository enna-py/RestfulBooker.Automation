namespace RestfulBooker.Data.DTO;

public sealed class RoomDto
{
    public int RoomId { get; init; }

    public string Type { get; set; } = string.Empty;

    public bool Accessible { get; init; }

    public string Image { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public IReadOnlyCollection<string> Features { get; set; } = [];

    public int RoomPrice { get; init; }
}