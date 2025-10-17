using Application.Guest.DTO;
using Application.Guest.Ports;
using Application.Guest.Request;
using Application.Guest.Responses;
using Domain.Exceptions;
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

                await guest.Save(_guestRepository);

                request.Data.Id = guest.Id;

                return new GuestResponse
                {
                    Data = request.Data,
                    Sucess = true
                };
            }
            catch (InvalidPersonDocumentIdException ex)
            {
                return new GuestResponse
                {
                    Sucess = false,
                    Message = "The ID passed is not valid",
                    ErrorCode = ErrorCodesEnum.INVALID_PERSON_ID
                };
            }
            catch (InvalidEmailException ex)
            {
                return new GuestResponse
                {
                    Sucess = false,
                    Message = "The given email is not valid",
                    ErrorCode = ErrorCodesEnum.INVALID_EMAIL
                };
            }
            catch (MissingRequiredInformationException ex)
            {
                return new GuestResponse
                {
                    Sucess = false,
                    Message = "Missing required fields",
                    ErrorCode = ErrorCodesEnum.MISSING_REQUIRED_DATA
                };
            }
            catch (Exception ex)
            {
                return new GuestResponse
                {
                    Sucess = false,
                    Message = "There was an error when saving to DB",
                    ErrorCode = ErrorCodesEnum.COULD_NOT_STORE_DATA
                };
            }
        }
    }
}
