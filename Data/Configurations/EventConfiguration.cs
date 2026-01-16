using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FluffySniffle.Models.Entities;

namespace FluffySniffle.Data.Configurations;

/// <summary>
/// EF Core configuration for Event entity.
/// </summary>
public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("Events");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .HasMaxLength(4000);

        builder.Property(e => e.Venue)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(e => e.ImageUrl)
            .HasMaxLength(1000);

        // Computed properties - không map xuống database
        builder.Ignore(e => e.IsPast);
        builder.Ignore(e => e.TotalAvailableTickets);
        builder.Ignore(e => e.CanBook);

        // Performance: Composite index cho query phổ biến "lấy events đang mở bán theo ngày"
        builder.HasIndex(e => e.EventDate);
        builder.HasIndex(e => e.Status);
        builder.HasIndex(e => new { e.Status, e.EventDate })
            .HasDatabaseName("IX_Event_Status_Date");
    }
}