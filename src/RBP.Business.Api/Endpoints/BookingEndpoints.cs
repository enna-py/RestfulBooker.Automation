namespace RBP.Business.Api.Endpoints;

public static class BookingEndpoints
{
    public const string Bookings = "/";

    public static string ByRoom(int roomId)
        => $"/?roomid={roomId}";

    public static string ById(int bookingId)
        => $"/{bookingId}";
}