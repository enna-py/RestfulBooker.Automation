namespace RestfulBooker.Data.Responses.Auth;

public sealed class LoginResponse
{
    public string Token { get; init; } = string.Empty;
}