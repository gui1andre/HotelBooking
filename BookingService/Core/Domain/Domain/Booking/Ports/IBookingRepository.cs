namespace Domain.Booking.Ports;

public interface IBookingRepository
{
    Task<int> Create(Entities.Booking booking);
    Task<Entities.Booking?> GetById(int id);
    Task Update(Entities.Booking booking);
}