using System.Threading;

namespace RestfulBooker.Core.Authentication;

public static class TokenProvider
{
    private static readonly AsyncLocal<string?> _token = new();

    public static string Token =>
        _token.Value ?? string.Empty;

    public static bool IsAuthenticated =>
        !string.IsNullOrWhiteSpace(_token.Value);

    public static void Authenticate(string token)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(token);

        _token.Value = token;
    }

    public static void SignOut()
    {
        _token.Value = null;
    }
}