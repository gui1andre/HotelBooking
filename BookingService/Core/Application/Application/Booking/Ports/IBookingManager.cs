using Application.Booking.DTO;
using Application.Booking.Request;
using Application.Booking.Responses;

namespace Application.Booking.Ports;

public interface IBookingManager
{
    Task<BookingResponse> Create(CreateBookingRequest request);
    Task<BookingResponse> GetById(int id);
}