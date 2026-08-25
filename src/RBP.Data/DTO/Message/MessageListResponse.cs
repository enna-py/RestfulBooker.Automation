using System.Text.Json.Serialization;

namespace RBP.Data.DTO.Message;

public sealed class MessageListResponse
{
    [JsonPropertyName("messages")]
    public List<MessageListItemDto> Messages { get; init; } = [];
}
