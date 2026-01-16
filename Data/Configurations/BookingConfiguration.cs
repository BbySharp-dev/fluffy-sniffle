using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FluffySniffle.Models.Entities;

namespace FluffySniffle.Data.Configurations;

/// <summary>
/// EF Core configuration for Booking entity.
/// </summary>
public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken();

        // Business Rule: BookingCode phải unique để tra cứu và check-in
        builder.HasIndex(b => b.BookingCode)
            .IsUnique();

        builder.Property(b => b.BookingCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(b => b.UnitPrice)
            .HasPrecision(18, 2);

        builder.Property(b => b.Notes)
            .HasMaxLength(500);

        builder.Property(b => b.QrCodeData)
            .HasMaxLength(1000);

        builder.Ignore(b => b.TotalAmount);
        builder.Ignore(b => b.CanCancel);
        builder.Ignore(b => b.CanCheckIn);

        // Restrict Delete: Không cho xóa User/TicketType nếu có Booking liên quan
        // Đảm bảo data integrity và audit trail
        builder.HasOne(b => b.User)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.TicketType)
            .WithMany(t => t.Bookings)
            .HasForeignKey(b => b.TicketTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Index cho các query phổ biến
        builder.HasIndex(b => b.UserId);
        builder.HasIndex(b => b.TicketTypeId);
        builder.HasIndex(b => b.Status);
        builder.HasIndex(b => b.CreatedAt);
    }
}