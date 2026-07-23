using AwesomeAssertions;
using RestfulBooker.Api.Clients;
using RestfulBooker.Core.Extensions;
using RestfulBooker.Data.DTO.Common;
using RestfulBooker.Data.DTO.Room;
using RestfulBooker.Tests.API;
using System.Net;

namespace RestfulBooker.Tests.Smoke.Api.Rooms;

[TestFixture]
public sealed class GetRoomsTests : BaseApiFixture
{
    private RoomApiClient roomApiClient;

    [SetUp]
    public override void Setup()
    {
        base.Setup();

        roomApiClient = new RoomApiClient();
    }

    [Test]
    public async Task GetRooms_ShouldReturnAvailableRooms()
    {
        // Arrange

        // Act
        ApiResponse<RoomsResponse> response =
            await roomApiClient.GetRoomsAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.IsSuccessful, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Data, Is.Not.Null);
            Assert.That(response.Data!.Rooms, Is.Not.Empty);
        });
    }

    [Test]
    public async Task User_Should_Login_Successfully()
    {
        // Act
        var response = await AuthApiClient.LoginAsync();

        // Assert
        response.ShouldBeOk();
    }
}