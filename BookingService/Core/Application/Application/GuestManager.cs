using Application.Guest.DTO;
using Application.Guest.Ports;
using Application.Guest.Request;
using Application.Guest.Responses;
using Domain.Ports;

namespace Application
{
    public class GuestManager : IGuestManager
    {
        private readonly IGuestRepository _guestRepository;
        public GuestManager(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }
        public async Task<GuestResponse> CreateGuest(CreateGuestRequest request)
        {
            try
            {
                var guest = GuestDTO.MapToEntity(request.Data);
                request.Data.Id = await _guestRepository.Create(guest);

                return new GuestResponse
                {
                    Data = request.Data,
                    Sucess = true
                };
            }
            catch (Exception ex)
            {
                return new GuestResponse
                {
                    Sucess = false,
                    Message = "There was an error when saving to DB",
                    ErrorCode = ErrorCodesEnum.COUL_DNOT_STORE_DAT
                };
            }
        }
    }
