using Domain.Enums;

namespace Domain.Entities;

public class Booking
{
    public Booking()
    {
        this.Status = StatusEnum.Created;
    }
    
    public int Id { get; set; }
    public DateTime PlacedAt { get; set; }
    public DateTime Start {get; set;}
    public DateTime End {get; set;}
    private StatusEnum Status {get; set;}
    
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
}