namespace GymSimulator.Application.DTOs;

public record StudentCreateDto(string Name, string? Email, string? Phone, Guid PlanTypeId);

public record StudentUpdateDto(string Name, string? Email, string? Phone, Guid PlanTypeId, bool IsActive);


