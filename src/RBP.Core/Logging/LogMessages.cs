public static class LogMessages
{
    public static string TestStarted(string name)
        => $"Test started: {name}";

    public static string TestFinished(string name)
        => $"Test finished: {name}";

    public static string ApiRequest(string method, string endpoint)
        => $"API Request: {method} {endpoint}";

    public static string ApiResponse(int statusCode, TimeSpan duration)
        => $"API Response: {statusCode} ({duration.TotalMilliseconds} ms)";

    public static string LoginStarted(string username)
        => $"Authentication started for user '{username}'.";

    public static string LoginSucceeded(string username)
        => $"Authentication succeeded for user '{username}'.";

    public static string LoginFailed(string username)
        => $"Authentication failed for user '{username}'.";

    public static string ScreenshotSaved(string path)
        => $"Screenshot saved: {path}";
}