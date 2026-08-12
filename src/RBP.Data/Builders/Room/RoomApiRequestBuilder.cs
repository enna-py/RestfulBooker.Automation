using RestfulBooker.Data.Builders.Base;
using RestfulBooker.Data.DTO.Room;

namespace RestfulBooker.Data.Builders.Room;

public sealed class RoomApiRequestBuilder : BaseBuilder<RoomApiRequest>
{
    private string _roomName = $"TC-{DateTime.UtcNow:HHmmssfff}";
    private string _type = "Single";
    private bool _accessible = true;
    private string _image = "/images/room1.jpg";
    private string _description = "Temporary room created by automated test";
    private int _roomPrice = 50;
    private IReadOnlyCollection<string> _features = ["WiFi"];

    public RoomApiRequestBuilder WithRoomName(string value)
    {
        _roomName = value;

        return this;
    }

    public RoomApiRequestBuilder WithType(string value)
    {
        _type = value;

        return this;
    }

    public RoomApiRequestBuilder WithDescription(string value)
    {
        _description = value;

        return this;
    }

    public RoomApiRequestBuilder WithPrice(int value)
    {
        _roomPrice = value;

        return this;
    }

    public override RoomApiRequest Build()
    {
        return new RoomApiRequest
        {
            RoomName = _roomName,
            Type = _type,
            Accessible = _accessible,
            Image = _image,
            Description = _description,
            RoomPrice = _roomPrice,
            Features = _features
        };
    }
}
