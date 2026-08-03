using RBP.Core.Helpers;
using RestfulBooker.Api.Base;
using RestfulBooker.Api.Endpoints;
using RestfulBooker.Core.Configuration;
using RestfulBooker.Data.DTO;
using RestfulBooker.Data.DTO.Common;
using RestfulBooker.Data.DTO.Room;

namespace RestfulBooker.Api.Clients;

public sealed class RoomApiClient : BaseApiClient
{
    public RoomApiClient()
        : base(ConfigurationService.Current.Api.RoomUrl)
    {
    }

    public async Task<IReadOnlyCollection<RoomDto>> GetRoomsAsync()
    {
        ApiResponse<RoomsResponse> response =
            await GetAsync<RoomsResponse>(RoomEndpoints.Rooms);

        foreach (RoomDto room in response.Data!.Rooms)
        {
            room.Type = StringNormalizer.Normalize(room.Type);
            room.Description = StringNormalizer.Normalize(room.Description);
            room.Image = StringNormalizer.Normalize(room.Image);
            room.Features = StringNormalizer.Normalize(room.Features).ToList();
        }

        return response.Data.Rooms;
    }

    public async Task<ApiResponse<RoomDto>> GetRoomAsync(int roomId)
    {
        return await GetAsync<RoomDto>(RoomEndpoints.ById(roomId));
    }
}