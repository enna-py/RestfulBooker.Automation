namespace RBP.Data.DTO.Message;

public sealed class ContactMessageRequest
{
    public string Name { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Phone { get; init; } = string.Empty;

    public string Subject { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;
}
