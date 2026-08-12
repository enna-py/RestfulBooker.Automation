using RBP.Core.Helpers;
using RestfulBooker.Api.Base;
using RestfulBooker.Api.Endpoints;
using RestfulBooker.Core.Authentication;
using RestfulBooker.Core.Configuration;
using RestfulBooker.Core.Exceptions;
using RestfulBooker.Data.DTO;
using RestfulBooker.Data.DTO.Common;
using RestfulBooker.Data.DTO.Room;

namespace RestfulBooker.Api.Clients;

public sealed class RoomApiClient : BaseApiClient
{
    public RoomApiClient(AuthenticationState authState)
        : base(ConfigurationService.Current.Api.RoomUrl, authState)
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

    public async Task<RoomDto> CreateRoomAsync(RoomApiRequest request)
    {
        ApiResponse<RoomDto> response =
            await PostAsync<RoomApiRequest, RoomDto>(
                RoomEndpoints.Rooms,
                request);

        if (response.Data is null)
        {
            throw new ApiException(
                (int)response.StatusCode,
                "Room was not created.");
        }

        return response.Data;
    }

    public async Task<ApiResponse<object>> DeleteRoomAsync(
        int roomId,
        bool validateResponse = true)
    {
        return await DeleteAsync<object>(
            RoomEndpoints.ById(roomId),
            validateResponse);
    }
}