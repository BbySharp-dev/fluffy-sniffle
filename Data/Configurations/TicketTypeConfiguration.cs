using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FluffySniffle.Models.Entities;

namespace FluffySniffle.Data.Configurations;

/// <summary>
/// EF Core configuration for TicketType entity.
/// CRITICAL: RowVersion là concurrency token để xử lý race condition khi đặt vé.
/// </summary>
public class TicketTypeConfiguration : IEntityTypeConfiguration<TicketType>
{
    public void Configure(EntityTypeBuilder<TicketType> builder)
    {
        builder.ToTable("TicketTypes");
        builder.HasKey(t => t.Id);

        // CRITICAL: Optimistic Concurrency - ngăn overselling khi nhiều user đặt cùng lúc
        // EF Core sẽ thêm RowVersion vào WHERE clause khi UPDATE
        builder.Property(t => t.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken();

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Description)
            .HasMaxLength(1000);

        // decimal(18,2) đủ cho giá vé lên tới 9,999,999,999,999,999.99
        builder.Property(t => t.Price)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(t => t.TotalQuantity)
            .IsRequired();

        builder.Property(t => t.AvailableQuantity)
            .IsRequired();

        // Computed properties - tính toán trong memory, không persist
        builder.Ignore(t => t.SoldQuantity);
        builder.Ignore(t => t.HasAvailableTickets);
        builder.Ignore(t => t.IsOnSale);
        builder.Ignore(t => t.CanBook);

        // N:1 - Nhiều loại vé thuộc 1 Event
        builder.HasOne(t => t.Event)
            .WithMany(e => e.TicketTypes)
            .HasForeignKey(t => t.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        // Performance: Composite index cho query "tìm vé còn chỗ của 1 event"
        builder.HasIndex(t => t.EventId);
        builder.HasIndex(t => new { t.EventId, t.AvailableQuantity })
            .HasDatabaseName("IX_TicketType_Event_Available");
    }
}