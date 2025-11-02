using Application.Guest.DTO;
using Application.Room.DTO;
using Domain.Enums;

namespace Application.Booking.DTO;

public class BookingDTO
{
    public BookingDTO()
    {
        this.PlacedAt = DateTime.UtcNow;
    }
    public int Id { get; set; }
    public DateTime PlacedAt { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int RoomId { get; set; }
    public int GuestId { get; set; }
    private StatusEnum Status { get; set; }

    public static Domain.Entities.Booking MapToEntity(BookingDTO dto)
    {
        return new Domain.Entities.Booking
        {
            Id = dto.Id,
            PlacedAt = dto.PlacedAt,
            Start = dto.Start,
            End = dto.End,
            Room = new Domain.Entities.Room {Id = dto.RoomId},
            Guest = new Domain.Entities.Guest {Id = dto.GuestId},
        };
    }

    public static BookingDTO MapToDTO(Domain.Entities.Booking entity)
    {
        return new BookingDTO
        {
            Id = entity.Id,
            PlacedAt = entity.PlacedAt,
            Start = entity.Start,
            End = entity.End,
            RoomId = entity.Room.Id,
            GuestId = entity.Guest.Id
        };
    }
}