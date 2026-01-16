namespace FluffySniffle.Models.Entities;

/// <summary>
/// Entity cơ sở - chứa các thuộc tính audit chung.
/// </summary>
public abstract class BaseEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();

    // Không cho phép set về tương lai
    public DateTime CreatedAt
    {
        get => field;
        init => field = value <= DateTime.UtcNow
            ? value
            : throw new ArgumentException("CreatedAt cannot be in the future");
    } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    // EF Core concurrency token
    public byte[] RowVersion { get; set; } = [];
}
