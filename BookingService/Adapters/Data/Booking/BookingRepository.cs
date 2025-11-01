using Domain.Booking.Ports;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Booking;

public class BookingRepository : IBookingRepository
{
    private readonly HotelDbContext _context;

    public BookingRepository(HotelDbContext context)
    {
        _context = context;
    }

    public async Task<int> Create(
        Domain.Entities.Booking booking)
    {
        _context.Add(booking);
        await _context.SaveChangesAsync();
        return booking.Id;
    }

    public async Task<Domain.Entities.Booking?> GetById(int id)
    {
       var booking = await _context.Bookings
           .Include(x => x.Guest)
           .Include(x => x.Room)
           .FirstOrDefaultAsync(x => x.Id  == id);

       return booking;
    }

    public Task Update(Domain.Entities.Booking booking)
    {
        throw new NotImplementedException();
    }
}