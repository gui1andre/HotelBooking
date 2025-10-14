using Domain.Entities;

namespace Domain.Ports;

public interface IGuestRepository
{
    Task<Guest> GetById(int id);
    Task<Guest> GetByEmail(string email);
    Task<int> Create(Domain.Entities.Guest guest);

}