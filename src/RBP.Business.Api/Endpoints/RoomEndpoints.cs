namespace RestfulBooker.Api.Endpoints;

public static class RoomEndpoints
{
    public const string Rooms = "/";

    public static string ById(int id)
        => $"/{id}";
}