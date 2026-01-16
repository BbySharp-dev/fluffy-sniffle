using FluffySniffle.Models.Enums;

namespace FluffySniffle.Models.Entities;

/// <summary>
/// Sự kiện (concert, show, hội nghị).
/// </summary>
public class Event : BaseEntity
{
    public required string Name
    {
        get => field;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value.Trim()
            : throw new ArgumentException("Event name cannot be empty");
    }

    public string? Description { get; set; }

    public required string Venue
    {
        get => field;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value.Trim()
            : throw new ArgumentException("Venue cannot be empty");
    }

    public required DateTime EventDate { get; set; }

    public string? ImageUrl { get; set; }

    public EventStatus Status { get; set; } = EventStatus.Draft;

    // Navigation
    public ICollection<TicketType> TicketTypes { get; set; } = [];

    // Computed
    public bool IsPast => EventDate < DateTime.UtcNow;
    public int TotalAvailableTickets => TicketTypes.Sum(t => t.AvailableQuantity);

    // Published + chưa qua + còn vé
    public bool CanBook => Status == EventStatus.Published && !IsPast && TotalAvailableTickets > 0;
}
