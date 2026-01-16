namespace FluffySniffle.Models.Entities;

/// <summary>
/// Loại vé của một sự kiện (VIP, Standard, Early Bird...).
/// </summary>
public class TicketType : BaseEntity
{
    public required string Name
    {
        get => field;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value.Trim()
            : throw new ArgumentException("Ticket type name cannot be empty");
    }

    public string? Description { get; set; }

    public decimal Price
    {
        get => field;
        set => field = value >= 0
            ? value
            : throw new ArgumentException("Price cannot be negative");
    }

    // Phải set TotalQuantity trước AvailableQuantity
    public int TotalQuantity
    {
        get => field;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Total quantity must be greater than zero");
            if (value < AvailableQuantity)
                throw new ArgumentException("Total quantity cannot be less than available quantity");
            field = value;
        }
    }

    public int AvailableQuantity
    {
        get => field;
        set
        {
            if (value < 0)
                throw new ArgumentException("Available quantity cannot be negative");
            if (value > TotalQuantity)
                throw new ArgumentException("Available quantity cannot exceed total quantity");
            field = value;
        }
    }

    public DateTime? SaleStartDate { get; set; }
    public DateTime? SaleEndDate { get; set; }

    // Navigation
    public Guid EventId { get; set; }
    public Event Event { get; set; } = null!;
    public ICollection<Booking> Bookings { get; set; } = [];

    // Computed
    public int SoldQuantity => TotalQuantity - AvailableQuantity;
    public bool HasAvailableTickets => AvailableQuantity > 0;

    public bool IsOnSale
    {
        get
        {
            var now = DateTime.UtcNow;
            var startOk = SaleStartDate == null || SaleStartDate <= now;
            var endOk = SaleEndDate == null || SaleEndDate >= now;
            return startOk && endOk;
        }
    }

    public bool CanBook => HasAvailableTickets && IsOnSale;
}
