using GymSimulator.Domain.Enums;

namespace GymSimulator.Domain.Entities;

public class Booking
{
    public int Id { get; set; }

    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public int ClassId { get; set; }
    public Class Class { get; set; } = null!;

    public BookingStatus BookingStatus { get; set; } = BookingStatus.Agendado;

    public DateTime BookingDate { get; set; } = DateTime.UtcNow;
    public DateTime? CancelledAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }

    public ICollection<BookingHistory> History { get; set; } = new List<BookingHistory>();
}
