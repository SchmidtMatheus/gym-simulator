namespace GymSimulator.Domain.Entities;

public class BookingHistory
{
    public Guid Id { get; set; }

    public Guid BookingId { get; set; }
    public Booking Booking { get; set; } = null!;

    public Guid StudentId { get; set; }
    public Guid ClassId { get; set; }

    public int? OldStatus { get; set; }
    public int NewStatus { get; set; }

    public string? ChangeReason { get; set; }
    public string? ChangedBy { get; set; }
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
}
