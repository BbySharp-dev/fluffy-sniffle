using FluffySniffle.Models.Enums;

namespace FluffySniffle.Models.Entities;

/// <summary>
/// Thông tin thanh toán.
/// </summary>
public class Payment : BaseEntity
{
    // Mã giao dịch từ cổng thanh toán (VnPay, Momo...)
    public string? TransactionId { get; set; }

    public PaymentMethod Method { get; set; }

    public decimal Amount
    {
        get => field;
        init => field = value > 0
            ? value
            : throw new ArgumentException("Amount must be greater than zero");
    }

    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

    public DateTime? PaidAt { get; set; }
    public string? FailureReason { get; set; }

    // JSON metadata từ payment gateway
    public string? Metadata { get; set; }

    // Navigation
    public Guid BookingId { get; init; }
    public Booking Booking { get; set; } = null!;
}