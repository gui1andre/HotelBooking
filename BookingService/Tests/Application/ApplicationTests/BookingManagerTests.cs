using Application.Booking;
using Application.Booking.DTO;
using Application.Payment.DTO;
using Application.Payment.Ports;
using Application.Payment.Responses;
using Domain.Booking.Ports;
using Domain.Ports;
using Moq;

namespace ApplicationTests;

public class BookingManagerTests
{
    
    [Test]
    public async Task Should_PayForABooking()
    {
        var dto = new PaymentRequestDTO()
        {
            SelectedPaymentProvider = SupportedPaymentProviders.MercadoPago,
            PaymentIntention = "https://www.mercadopago.com.br/asdf",
            SelectedPaymentMethod = SupportedPaymentMethods.Credit
        };

        var bookingRepository = new Mock<IBookingRepository>();
        var roomRepository = new Mock<IRoomRepository>();
        var guestRepository = new Mock<IGuestRepository>();
        var paymentProcessorFactory = new Mock<IPaymentProcessorFactory>();
        var paymentProcessor = new Mock<IPaymentProcessor>();

        var responseDto = new PaymentStateDTO
        {
            CreatedDate = DateTime.Now,
            Message = $"Successfully paid {dto.PaymentIntention}",
            PaymentId = "123",
            Status = Status.Sucess
        };

        var response = new PaymentResponse
        {
            Data = responseDto,
            Sucess = true,
            Message = "Payment successfully processed"
        };

        paymentProcessor.
            Setup(x => x.CapturePayment(dto.PaymentIntention))
            .Returns(Task.FromResult(response));

        paymentProcessorFactory
            .Setup(x => x.GetPaymentProcessor(dto.SelectedPaymentProvider))
            .Returns(paymentProcessor.Object);

        var bookingManager = new BookingManager(
            bookingRepository.Object,
            roomRepository.Object,
            guestRepository.Object,
            paymentProcessorFactory.Object);

        var res = await bookingManager.PayForABooking(dto);

        Assert.That(res, Is.Not.Null);
        Assert.That(res.Sucess, Is.True);
        Assert.That(res.Message, Is.EqualTo("Payment successfully processed"));
    }
}