namespace FluffySniffle.Models.Enums;

/// <summary>
/// Trạng thái thanh toán.
/// </summary>
public enum PaymentStatus
{
    /// <summary>
    /// Chờ thanh toán.
    /// </summary>
    Pending = 0,

    /// <summary>
    /// Đang xử lý.
    /// </summary>
    Processing = 1,

    /// <summary>
    /// Thanh toán thành công.
    /// </summary>
    Completed = 2,

    /// <summary>
    /// Thanh toán thất bại.
    /// </summary>
    Failed = 3,

    /// <summary>
    /// Đã hoàn tiền.
    /// </summary>
    Refunded = 4
}