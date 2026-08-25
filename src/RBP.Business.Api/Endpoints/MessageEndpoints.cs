namespace RestfulBooker.Api.Endpoints;

public static class MessageEndpoints
{
    public const string Messages = "/";

    public static string ById(int messageId)
        => $"/{messageId}";
}
