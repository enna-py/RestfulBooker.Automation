namespace RestfulBooker.Data.DTO.Auth;

public sealed class TokenRequest
{
    public string Token { get; init; } = string.Empty;
}