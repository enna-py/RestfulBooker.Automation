using RBP.Data.DTO.Booking;
using RestfulBooker.Data.Builders.Base;

namespace RestfulBooker.Data.Builders.Booking;

public sealed class BookingRequestBuilder : BaseBuilder<BookingRequest>
{
    private int _roomId = 1;
    private DateOnly _checkIn = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
    private DateOnly _checkOut = DateOnly.FromDateTime(DateTime.Today.AddDays(2));
    private string _firstName = "Test";
    private string _lastName = "Test";
    private string _email = "test.guest@example.com";
    private string _phone = "012345678901";

    public BookingRequestBuilder WithRoomId(int value)
    {
        _roomId = value;

        return this;
    }

    public BookingRequestBuilder WithDates(DateOnly checkIn, DateOnly checkOut)
    {
        _checkIn = checkIn;
        _checkOut = checkOut;

        return this;
    }

    public BookingRequestBuilder WithGuestName(string firstName, string lastName)
    {
        _firstName = firstName;
        _lastName = lastName;

        return this;
    }

    public BookingRequestBuilder WithEmail(string value)
    {
        _email = value;

        return this;
    }

    public BookingRequestBuilder WithPhone(string value)
    {
        _phone = value;

        return this;
    }

    public override BookingRequest Build()
    {
        return new BookingRequest
        {
            RoomId = _roomId,
            CheckIn = _checkIn,
            CheckOut = _checkOut,
            Guest = new GuestDto
            {
                FirstName = _firstName,
                LastName = _lastName,
                Email = _email,
                Phone = _phone
            }
        };
    }
}
