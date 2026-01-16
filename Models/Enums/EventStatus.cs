namespace FluffySniffle.Models.Enums;

/// <summary>
/// Trạng thái của sự kiện.
/// </summary>
public enum EventStatus
{
    /// <summary>
    /// Bản nháp, chưa công khai.
    /// </summary>
    Draft = 0,

    /// <summary>
    /// Đã công khai, đang mở bán.
    /// </summary>
    Published = 1,

    /// <summary>
    /// Đã bán hết vé.
    /// </summary>
    SoldOut = 2,

    /// <summary>
    /// Đã hủy.
    /// </summary>
    Cancelled = 3,

    /// <summary>
    /// Đã kết thúc.
    /// </summary>
    Completed = 4
}