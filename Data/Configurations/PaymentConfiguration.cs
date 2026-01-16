using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FluffySniffle.Models.Entities;

namespace FluffySniffle.Data.Configurations;

/// <summary>
/// EF Core configuration for Payment entity.
/// </summary>
public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken();

        // TransactionId từ payment gateway (VnPay, Momo, Stripe...)
        builder.Property(p => p.TransactionId)
            .HasMaxLength(100);

        builder.Property(p => p.Amount)
            .HasPrecision(18, 2);

        builder.Property(p => p.FailureReason)
            .HasMaxLength(500);

        // JSON response từ payment gateway để debug/audit
        builder.Property(p => p.Metadata)
            .HasMaxLength(4000);

        // 1:1 - Mỗi Booking chỉ có 1 Payment
        // Cascade Delete: Xóa Booking sẽ xóa luôn Payment
        builder.HasOne(p => p.Booking)
            .WithOne(b => b.Payment)
            .HasForeignKey<Payment>(p => p.BookingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(p => p.TransactionId);
        builder.HasIndex(p => p.Status);
    }
}