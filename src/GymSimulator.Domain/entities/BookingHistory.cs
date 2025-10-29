namespace GymSimulator.Domain.Entities;

public class BookingHistory
{
    public int Id { get; set; }

    public int BookingId { get; set; }
    public Booking Booking { get; set; } = null!;

    public int StudentId { get; set; }
    public int ClassId { get; set; }

    public int? OldStatus { get; set; }
    public int NewStatus { get; set; }

    public string? ChangeReason { get; set; }
    public string? ChangedBy { get; set; }
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
}
