using Entities = Domain.Entities;
using Domain.Enums;

namespace Application.Guest.DTO
{
    public class GuestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string IdNumber { get; set; }
        public int IdTypeCode { get; set; }

        public static Entities.Guest MapToEntity(GuestDTO dto)
        {
            return new Entities.Guest
            {
                Id = dto.Id,
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                DocumentId = new Domain.ValueObjects.PersonId
                {
                    IdNumber = dto.IdNumber,
                    DocumentType = (DocumentType)dto.IdTypeCode
                }
            };
        }

        public static GuestDTO MapToDTO(Entities.Guest entity)
        {
            return new GuestDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                Email = entity.Email,
                IdNumber = entity.DocumentId.IdNumber,
                IdTypeCode = (int)entity.DocumentId.DocumentType
            };
        }

    }
}
