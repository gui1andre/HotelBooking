using Application;
using Application.Room.DTO;
using Application.Room.Ports;
using Application.Room.Request;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly ILogger<RoomController> _logger;
    private readonly IRoomManager _roomManager;

    public RoomController(ILogger<RoomController> logger, IRoomManager roomManager)
    {
        _logger = logger;
        _roomManager = roomManager;
    }

    [HttpPost]
    public async Task<ActionResult> Post(RoomDTO room)
    {
        var request = new CreateRoomRequest
        {
            Data = room
        };
        var res = await _roomManager.CreateRoom(request);

        if (res.Sucess) return Created("", res.Data);

        if (res.ErrorCode == ErrorCodesEnum.ROOM_MISSING_REQUIRED_INFORMATION)
            return BadRequest(res);

        if (res.ErrorCode == ErrorCodesEnum.ROOM_COULD_NOT_STORE_DATA)
            return BadRequest(res);
        
        _logger.LogError("Response with unknown ErrorCode Returned", res);
        return BadRequest(500);
    }

    [HttpGet]
    public async Task<ActionResult> Get(int roomId)
    {
        var room = await _roomManager.GetRoom(roomId);
        
        if (room == null) return NotFound();
        
        return Ok(room.Data);
    }
}