using Application.Booking.DTO;
using Application.Booking.Ports;
using Application.Booking.Request;
using Application.Booking.Responses;
using Domain.Booking.Exceptions;
using Domain.Booking.Ports;
using Domain.Exceptions;
using Domain.Ports;

namespace Application.Booking;

public class BookingManager : IBookingManager
{
    private readonly IBookingRepository  _bookingRepository;
    private readonly IRoomRepository  _roomRepository;
    private readonly IGuestRepository  _guestRepository;

    public BookingManager(
        IBookingRepository bookingRepository,
        IRoomRepository roomRepository,
        IGuestRepository guestRepository)
    {
        _bookingRepository = bookingRepository;
        _roomRepository = roomRepository;
        _guestRepository = guestRepository;
    }
    
    public async Task<BookingResponse> Create(CreateBookingRequest request)
    {
        try
        {
            var booking = BookingDTO.MapToEntity(request.Data);

            booking.Room = await _roomRepository.GetRoom(request.Data.RoomId);
            booking.Guest = await _guestRepository.GetById(request.Data.GuestId);

            await booking.Save(_bookingRepository);

            request.Data.Id = booking.Id;

            return new BookingResponse
            {
                Sucess = true,
                Data = request.Data
            };
        }
        catch (EndIsRequiredException e)
        {
            return new BookingResponse
            {
                Sucess = false,
                ErrorCode = ErrorCodesEnum.BOOKING_MISSING_REQUIRED_INFORMATION,
                Message = "End is a required field"
            };
        }
        catch (PlacedAtIsRequiredException e)
        {
            return new BookingResponse
            {
                Sucess = false,
                ErrorCode = ErrorCodesEnum.BOOKING_MISSING_REQUIRED_INFORMATION,
                Message = "PlacedAt is a required field"
            };
        }
        catch (RoomCannotBeBookedException e)
        {
            return new BookingResponse
            {
                Sucess = false,
                ErrorCode = ErrorCodesEnum.ROOM_CAN_NOT_BE_BOOKED,
                Message = "Room cannot be booked"
            };
        }
        catch (StartIsRequiredException e)
        {
            return new BookingResponse
            {
                Sucess = false,
                ErrorCode = ErrorCodesEnum.BOOKING_MISSING_REQUIRED_INFORMATION,
                Message = "Start is a required field"
            };
        }
        catch (RoomIsRequiredException e)
        {
            return new BookingResponse
            {
                Sucess = false,
                ErrorCode = ErrorCodesEnum.BOOKING_MISSING_REQUIRED_INFORMATION,
                Message = "Room is a required field"
            };
        }
        catch (GuestIsRequiredException e)
        {
            return new BookingResponse
            {
                Sucess = false,
                ErrorCode = ErrorCodesEnum.BOOKING_MISSING_REQUIRED_INFORMATION,
                Message = "Guest is a required field"
            };
        }
        catch (Exception e)
        {
            return new BookingResponse
            {
                Sucess = false,
                ErrorCode = ErrorCodesEnum.BOOKING_COULD_NOT_STORE_DATA
            };
        }
    }

    public async Task<BookingResponse> GetById(int id)
    {
        try
        {
            var booking = await _bookingRepository.GetById(id);

            if (booking is null)
            {
                return new BookingResponse
                {
                    Sucess = false,
                    ErrorCode = ErrorCodesEnum.BOOKING_NOT_FOUND
                
                };
            }

            return new BookingResponse
            {
                Sucess = true,
                Data = BookingDTO.MapToDTO(booking)
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}