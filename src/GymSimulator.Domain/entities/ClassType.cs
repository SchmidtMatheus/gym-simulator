namespace GymSimulator.Domain.Entities;

public class ClassType
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }

    // Relacionamento 1:N com Classes
    public ICollection<Class> Classes { get; set; } = new List<Class>();
}
