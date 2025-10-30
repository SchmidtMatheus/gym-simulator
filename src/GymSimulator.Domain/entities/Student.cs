namespace GymSimulator.Domain.Entities;

public class Student
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }

    public Guid PlanTypeId { get; set; }
    public PlanType PlanType { get; set; } = null!;

    public bool IsActive { get; set; } = true;
    public int MonthlyClassCount { get; set; } = 0;
    public string CurrentMonthYear { get; set; } = DateTime.UtcNow.ToString("yyyyMM");
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }

    // Relacionamento 1:N com Bookings
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
