using System.Text.Json.Serialization;

namespace RestfulBooker.Data.DTO.Room;

public sealed class RoomApiRequest
{
    [JsonPropertyName("roomName")]
    public string RoomName { get; init; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("accessible")]
    public bool Accessible { get; init; }

    [JsonPropertyName("image")]
    public string Image { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; init; } = string.Empty;

    [JsonPropertyName("roomPrice")]
    public int RoomPrice { get; init; }

    [JsonPropertyName("features")]
    public IReadOnlyCollection<string> Features { get; init; } = [];
}
