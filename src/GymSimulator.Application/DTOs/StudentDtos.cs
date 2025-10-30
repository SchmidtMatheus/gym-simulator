namespace GymSimulator.Application.DTOs;

public class StudentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PlanTypeName { get; set; } = string.Empty;

}

public record StudentCreateDto(string Name, string? Email, string? Phone, Guid PlanTypeId);

public record StudentUpdateDto(string Name, string? Email, string? Phone, Guid PlanTypeId, bool IsActive);


