using System.Text.Json.Serialization;

namespace RBP.Data.DTO.Message;

public sealed class MessageListItemDto
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("subject")]
    public string Subject { get; init; } = string.Empty;

    [JsonPropertyName("read")]
    public bool Read { get; init; }
}
