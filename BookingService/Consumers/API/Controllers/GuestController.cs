using Application;
using Application.Guest.DTO;
using Application.Guest.Ports;
using Application.Guest.Request;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly ILogger<GuestController> _logger;
        private readonly IGuestManager _guestManager;
        
        public GuestController(ILogger<GuestController> logger, IGuestManager guestManager)
        {
            _logger = logger;
            _guestManager = guestManager;
        }


        [HttpPost]
        public async Task<ActionResult<GuestDTO>> Post(GuestDTO request)
        {
            var guest = new CreateGuestRequest
            {
                Data = request
            };
            
            var res = await _guestManager.CreateGuest(guest);
            
            if (res.Sucess) return Created("", res.Data);

            if (res.ErrorCode == ErrorCodesEnum.NOT_FOUND)
                return NotFound(res);
            
            if(res.ErrorCode == ErrorCodesEnum.INVALID_PERSON_ID)
                return BadRequest(res);
            
            if(res.ErrorCode == ErrorCodesEnum.MISSING_REQUIRED_DATA)
                return BadRequest(res);
            
            if(res.ErrorCode == ErrorCodesEnum.INVALID_EMAIL)
                return BadRequest(res);
            
            if(res.ErrorCode == ErrorCodesEnum.COULD_NOT_STORE_DATA)
                return BadRequest(res);
            
            _logger.LogError("Response with unknown ErrorCode returned", res);
            return BadRequest(500);
        }

        [HttpGet]
        public async Task<ActionResult<GuestDTO>> Get(int guestId)
        {
            var res = await _guestManager.GetById(guestId);
            
            if (res.Sucess) return Created("", res.Data);

            return NotFound(res);


        }
    }
}
