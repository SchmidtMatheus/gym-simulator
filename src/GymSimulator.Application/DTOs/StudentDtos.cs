namespace GymSimulator.Application.DTOs;

public record StudentCreateDto(string Name, string? Email, string? Phone, int PlanTypeId);

public record StudentUpdateDto(string Name, string? Email, string? Phone, int PlanTypeId, bool IsActive);


