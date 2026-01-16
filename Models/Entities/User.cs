namespace FluffySniffle.Models.Entities;

/// <summary>
/// Người dùng hệ thống.
/// </summary>
public class User : BaseEntity
{
    // Unique, lowercase - dùng làm login identifier
    public required string Email
    {
        get => field;
        set => field = !string.IsNullOrWhiteSpace(value) && value.Contains('@')
            ? value.ToLowerInvariant().Trim()
            : throw new ArgumentException("Invalid email format");
    }

    public required string PasswordHash { get; set; }

    public required string FullName
    {
        get => field;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value.Trim()
            : throw new ArgumentException("FullName cannot be empty");
    }

    public string? PhoneNumber { get; set; }

    public bool IsLocked { get; set; }
    public string? LockReason { get; set; }

    // JWT refresh token
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    // Navigation
    public ICollection<Booking> Bookings { get; set; } = [];
}
