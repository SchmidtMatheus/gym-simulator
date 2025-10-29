namespace GymSimulator.Domain.Entities;

public class PlanType
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int MonthlyClassLimit { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }

    // Relacionamento 1:N com Students
    public ICollection<Student> Students { get; set; } = new List<Student>();
}
