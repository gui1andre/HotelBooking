using Domain.Enums;

namespace Application.Room.DTO;

public class RoomDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public bool InMaintenance { get; set; }
    public decimal Price { get; set; }
    public AcceptedCurrencies Currency { get; set; }

    public static Domain.Entities.Room MapToEntity(RoomDTO dto)
    {
        return new Domain.Entities.Room
        {
            Id = dto.Id,
            Name = dto.Name,
            Level = dto.Level,
            InMaintenance = dto.InMaintenance,
            Price = new Domain.ValueObjects.Price { Currency = dto.Currency, Value = dto.Price }
        };
    }

    public static RoomDTO MapToDTO(Domain.Entities.Room entity)
    {
        return new RoomDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            Level = entity.Level,
            InMaintenance = entity.InMaintenance,
            Price = entity.Price.Value,
            Currency = entity.Price.Currency
        };
    }
}