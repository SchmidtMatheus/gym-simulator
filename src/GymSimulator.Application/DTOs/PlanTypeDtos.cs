namespace GymSimulator.Application.DTOs;

public record PlanTypeCreateDto(string Name, string? Description, int MonthlyClassLimit, bool IsActive = true);

public record PlanTypeUpdateDto(string Name, string? Description, int MonthlyClassLimit, bool IsActive);


