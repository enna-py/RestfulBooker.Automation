using RBP.Data.DTO.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBP.Tests.Ui.Builders.Room;

public sealed class EditRoomDataBuilder
{
    private int _id;
    private string _type = "room type";

    private string _description =
        "Updated room description";

    private decimal _price = 150;

    private string _image = "/images/room1.jpg";

    private IReadOnlyCollection<string> _features =
    [
        "TV",
        "WiFi",
        "Views"
    ];

    public EditRoomDataBuilder WithId(int value)
    {
        _id = value; 

        return this;
    }
    public EditRoomDataBuilder WithType(string value)
    {
        _type = value;

        return this;
    }

    public EditRoomDataBuilder WithDescription(string value)
    {
        _description = value;

        return this;
    }

    public EditRoomDataBuilder WithPrice(decimal value)
    {
        _price = value;

        return this;
    }

    public EditRoomDataBuilder WithImage(string value)
    {
        _image = value;

        return this;
    }

    public EditRoomDataBuilder WithFeatures(params string[] values)
    {
        _features = values;

        return this;
    }

    public RoomCardDto Build()
    {
        return new RoomCardDto
        {
            Description = _description,
            Price = _price,
            Image = _image,
            Features = _features
        };
    }
}
