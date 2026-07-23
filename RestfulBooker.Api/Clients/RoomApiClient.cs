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

    public async Task<ApiResponse<RoomsResponse>> GetRoomsAsync()
    {
        return await GetAsync<RoomsResponse>(
            RoomEndpoints.Rooms);
    }

    public async Task<ApiResponse<RoomDto>> GetRoomAsync(int roomId)
    {
        return await GetAsync<RoomDto>(RoomEndpoints.ById(roomId));
    }
}