using Application;
using Application.Booking.DTO;
using Application.Booking.Ports;
using Application.Booking.Request;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    private readonly ILogger<BookingController> _logger;
    private readonly IBookingManager _bookingManager;

    public BookingController(IBookingManager bookingManager, ILogger<BookingController> logger)
    {
        _logger = logger;
        _bookingManager = bookingManager;
    }

    [HttpPost]
    public async Task<ActionResult<BookingDTO>> Post(CreateBookingRequest request)
    {
        var res = await _bookingManager.Create(request);

        if (res.Sucess) return Created("", res.Data);

        if (res.ErrorCode == ErrorCodesEnum.BOOKING_COULD_NOT_STORE_DATA)
            return BadRequest(res);
        
        if (res.ErrorCode == ErrorCodesEnum.BOOKING_MISSING_REQUIRED_INFORMATION)
            return BadRequest(res);
        
        _logger.LogError("Response with unknow errorcode returned", res);

        return BadRequest(res);
        
    }

    [HttpGet]
    public async Task<ActionResult<BookingDTO>> Get(int id)
    {
        var res = await _bookingManager.GetById(id);
        
        if (res.Sucess) return Ok(res.Data);
        
        return NotFound(res);
    }
    
}