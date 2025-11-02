namespace Application.Booking.DTO;

public enum SupportedPaymentProviders
{
    MercadoPago = 0,
    Stripe = 1
}

public enum SupportedPaymentMethods
{
    Debit = 0,
    Credit = 1,
    BankTransfer = 2
}

public class PaymentRequestDTO
{
    public int BookingId { get; set; }
    public string PaymentIntention { get; set; }
    public SupportedPaymentProviders SelectedPaymentProvider { get; set; }
    public SupportedPaymentMethods SelectedPaymentMethod { get; set; }
}