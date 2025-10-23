using Domain.Exceptions;
using Domain.Ports;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public bool InMaintenance { get; set; }
    public Price Price { get; set; }
    public bool IsAvailable
    {
        get
        {
            return this.InMaintenance && !this.HasGuest;
        }
    }

    public bool HasGuest
    {
        get { return true; }
    }

    private void ValidateState()
    {
        if (string.IsNullOrEmpty(this.Name))
            throw new InvalidRoomDataException();
    }

    public async Task Save(IRoomRepository repository)
    {
        ValidateState();

        if (this.Id == 0)
            this.Id = await repository.CreateRoom(this);
        else
            throw new NotImplementedException();
    }
}