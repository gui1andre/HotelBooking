using Domain.Entities;

namespace Domain.Ports;

public interface IRoomRepository
{
    Task<int> CreateRoom(Room room);
    Task<Room?> GetRoom(int roomId);
}