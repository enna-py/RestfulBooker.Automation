namespace RestfulBooker.Core.Authentication;

public sealed class AuthenticationState
{
    public string Token { get; set; } = string.Empty;

    public bool IsAuthenticated =>
        !string.IsNullOrWhiteSpace(Token);
}
