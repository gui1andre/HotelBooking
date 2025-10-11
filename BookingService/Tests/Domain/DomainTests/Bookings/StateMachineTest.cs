using Domain.Entities;
using Domain.Enums;

namespace DomainTests.Bookings;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ShouldAlwaysStartWithCreatedStatus()
    {
        var booking = new Booking();

        Assert.That(booking.CurrentStatus, Is.EqualTo(StatusEnum.Created));
    }

    [Test]
    public void ShouldSetStatusToPaidWhenPayingForABookingWithCreatedStatus()
    {
        var booking = new Booking();
        
        booking.ChangeState(ActionEnum.Pay);
        
        Assert.That(booking.CurrentStatus, Is.EqualTo(StatusEnum.Paid));
    }
    
    [Test]
    public void ShouldSetStatusToCanceldWhenCacelingABookingWithCreatedStatus()
    {
        var booking = new Booking();
        
        booking.ChangeState(ActionEnum.Cancel);
        
        Assert.That(booking.CurrentStatus, Is.EqualTo(StatusEnum.Canceled));
    }
    
    [Test]
    public void ShouldSetStatusToFinishedWhenFinishingAPaidBooking()
    {
        var booking = new Booking();
        
        booking.ChangeState(ActionEnum.Pay);
        booking.ChangeState(ActionEnum.Finish);
        
        Assert.That(booking.CurrentStatus, Is.EqualTo(StatusEnum.Finished));
    }
    
    [Test]
    public void ShouldSetStatusToRefoundedWhenRefoundingAPaidBOooking()
    {
        var booking = new Booking();
        
        booking.ChangeState(ActionEnum.Pay);
        booking.ChangeState(ActionEnum.Refound);
        
        Assert.That(booking.CurrentStatus, Is.EqualTo(StatusEnum.Refounded));
    }
    
    [Test]
    public void ShouldSetStatusToCreatedWhenReopingACanceledBooking()
    {
        var booking = new Booking();
        
        booking.ChangeState(ActionEnum.Cancel);
        booking.ChangeState(ActionEnum.Reopen);
        
        Assert.That(booking.CurrentStatus, Is.EqualTo(StatusEnum.Created));
    }
    
    [Test]
    public void ShouldNotChangeStatusWhenRefoundingABookingWithCreatedStatus()
    {
        var booking = new Booking();
        
        booking.ChangeState(ActionEnum.Refound);
        
        Assert.That(booking.CurrentStatus, Is.EqualTo(StatusEnum.Created));
    }
    
    [Test]
    public void ShouldNotChangeStatusWhenRefoingAFinishedBooking()
    {
        var booking = new Booking();
        
        booking.ChangeState(ActionEnum.Pay);
        booking.ChangeState(ActionEnum.Finish);
        booking.ChangeState(ActionEnum.Refound);
        
        Assert.That(booking.CurrentStatus, Is.EqualTo(StatusEnum.Finished));
        
    }
}