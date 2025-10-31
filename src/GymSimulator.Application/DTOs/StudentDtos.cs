namespace GymSimulator.Application.DTOs;

public class StudentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string PlanTypeName { get; set; } = string.Empty;

}

public class StudentReportDto
{
    public Guid StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string StudentEmail { get; set; } = string.Empty;
    public int TotalClassesThisMonth { get; set; }
    public List<ClassTypeFrequencyDto> MostFrequentClassTypes { get; set; } = new();
    public DateTime ReportDate { get; set; } = DateTime.UtcNow;
}

public record StudentCreateDto(string Name, string? Email, string? Phone, Guid PlanTypeId);

public record StudentUpdateDto(string Name, string? Email, string? Phone, Guid PlanTypeId, bool IsActive);


