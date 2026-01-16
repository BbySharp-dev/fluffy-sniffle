namespace FluffySniffle.Models.Enums;

/// <summary>
/// Trạng thái đơn đặt vé.
/// </summary>
public enum BookingStatus
{
    /// <summary>
    /// Đang chờ thanh toán.
    /// </summary>
    Pending = 0,

    /// <summary>
    /// Đã thanh toán, vé đã xác nhận.
    /// </summary>
    Confirmed = 1,

    /// <summary>
    /// Đã hủy đơn.
    /// </summary>
    Cancelled = 2,

    /// <summary>
    /// Đã check-in vào sự kiện.
    /// </summary>
    CheckedIn = 3,

    /// <summary>
    /// Hết hạn thanh toán.
    /// </summary>
    Expired = 4
}