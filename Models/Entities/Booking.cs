using FluffySniffle.Models.Enums;

namespace FluffySniffle.Models.Entities;

public class Booking : BaseEntity
{

    public string BookingCode { get; init; } = GenerateBookingCode();


    public int Quantity
    {
        get => field;
        init => field = value > 0 && value <= 10
            ? value
            : throw new ArgumentException("Quantity must be between 1 and 10");
    }

    public decimal UnitPrice { get; init; }


    public decimal TotalAmount => Quantity * UnitPrice;


    public BookingStatus Status { get; set; } = BookingStatus.Pending;


    public string? QrCodeData { get; set; }


    public string? Notes { get; set; }


    public Guid UserId { get; init; }
    public User User { get; set; } = null!;

    public Guid TicketTypeId { get; init; }
    public TicketType TicketType { get; set; } = null!;

    public Payment? Payment { get; set; }

    private static string GenerateBookingCode()
    {
        var datePart = DateTime.UtcNow.ToString("yyyyMMdd");
        var randomPart = Guid.NewGuid().ToString("N")[..6].ToUpperInvariant();
        return $"TH-{datePart}-{randomPart}";
    }

    public bool CanCancel => Status == BookingStatus.Pending;


    public bool CanCheckIn => Status == BookingStatus.Confirmed;
}