namespace GymSimulator.Domain.Entities;

public class Class
{
    public Guid Id { get; set; }
    public Guid ClassTypeId { get; set; }
    public ClassType ClassType { get; set; } = null!;
    public DateTime ScheduledAt { get; set; }
    public int DurationMinutes { get; set; } = 60;
    public int MaxCapacity { get; set; }
    public int CurrentParticipants { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    public bool IsCancelled { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
