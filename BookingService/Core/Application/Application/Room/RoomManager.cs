using Application.Room.DTO;
using Application.Room.Ports;
using Application.Room.Request;
using Application.Room.Responses;
using Domain.Exceptions;
using Domain.Ports;

namespace Application.Room;

public class RoomManager : IRoomManager
{
    private readonly IRoomRepository _roomRepository;

    public RoomManager(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }
    public async Task<RoomResponse> CreateRoom(CreateRoomRequest request)
    {
        try
        {
            var room = RoomDTO.MapToEntity(request.Data);

            await _roomRepository.CreateRoom(room);

            request.Data.Id = room.Id;

            return new RoomResponse
            {
                Sucess = true,
                Data = request.Data
            };
        }
        catch (InvalidRoomDataException e)
        {
            return new RoomResponse
            {
                Sucess = false,
                ErrorCode = ErrorCodesEnum.ROOM_MISSING_REQUIRED_INFORMATION,
                Message = "Missing required information passed"
            };
        }
        catch (Exception e)
        {
            return new RoomResponse
            {
                Sucess = false,
                ErrorCode = ErrorCodesEnum.ROOM_COULD_NOT_STORE_DATA,
                Message = "There was an error when saving to DB"
            };
        }
    }

    public async Task<RoomResponse?> GetRoom(int roomId)
    {
        var room = await _roomRepository.GetRoom(roomId);

        if (room is null)
        {
            return new RoomResponse
            {
                Sucess = false,
                ErrorCode = ErrorCodesEnum.ROOM_NOT_FOUND,
                Message = "Room not found"
            };
        }

        return new RoomResponse
        {
            Sucess = true,
            Data = RoomDTO.MapToDTO(room)
        };
    }
}