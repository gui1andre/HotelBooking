using Domain.Booking.Exceptions;
using Domain.Booking.Ports;
using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities;

public class Booking
{
    public Booking()
    {
        this.Status = StatusEnum.Created;
        this.PlacedAt =  DateTime.UtcNow;
    }
    
    public int Id { get; set; }
    public DateTime PlacedAt { get; set; }
    public DateTime Start {get; set;}
    public DateTime End {get; set;}
    public Room Room {get; set; }
    public Guest Guest {get; set; }
    public StatusEnum Status {get; set;}
    
    public StatusEnum CurrentStatus
    {
        get { return this.Status; }
    }

    public void ChangeState(ActionEnum action)
    {
        this.Status = (this.Status, action) switch
        {
            (StatusEnum.Created, ActionEnum.Pay) => this.Status = StatusEnum.Paid,
            (StatusEnum.Created, ActionEnum.Cancel) => this.Status = StatusEnum.Canceled,
            (StatusEnum.Paid, ActionEnum.Finish) => this.Status = StatusEnum.Finished,
            (StatusEnum.Paid, ActionEnum.Refound) => this.Status = StatusEnum.Refounded,
            (StatusEnum.Canceled, ActionEnum.Reopen) => this.Status = StatusEnum.Created,
            _ => this.Status
        };
    }

    private void ValidateState()
    {
        if (PlacedAt == default(DateTime))
            throw new PlacedAtIsRequiredException();
        
        if(Start == default(DateTime))
            throw new StartIsRequiredException();
        
        if(End == default(DateTime))
            throw new EndIsRequiredException();
        
        if(Room == null)
            throw new RoomIsRequiredException();
        
        Room.IsValid();
        
        if(Guest == null)
            throw new GuestIsRequiredException();
        
        Guest.IsValid();
    }

    public async Task Save(IBookingRepository repository)
    {
        this.ValidateState();
        
        this.Guest.IsValid();

        if (!this.Room.CanBeBooked())
            throw new RoomCannotBeBookedException();
        
        if (this.Id == 0)
        {
           var id = await repository.Create(this);
           this.Id = id;
        }
        else
            repository.Update(this);
    }
}