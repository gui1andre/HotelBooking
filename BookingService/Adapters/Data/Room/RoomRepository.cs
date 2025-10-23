using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Room;

public class RoomRepository : IRoomRepository
{
    private readonly HotelDbContext _context;

    public RoomRepository(HotelDbContext context)
    {
        _context = context;
    }
    public async Task<int> CreateRoom(Domain.Entities.Room room)
    {
        _context.Add(room);
        await _context.SaveChangesAsync();
        return room.Id;
    }

    public Task<Domain.Entities.Room?> GetRoom(int roomId)
    {
        return _context.Rooms.FirstOrDefaultAsync(x => x.Id == roomId);
    }
}