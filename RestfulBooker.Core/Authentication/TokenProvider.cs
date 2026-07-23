namespace RestfulBooker.Core.Authentication;

public static class TokenProvider
{
    private static string _token = string.Empty;

    public static string Token => _token;

    public static bool IsAuthenticated =>
        !string.IsNullOrWhiteSpace(_token);

    public static void Authenticate(string token)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(token);

        _token = token;
    }

    public static void SignOut()
    {
        _token = string.Empty;
    }
}