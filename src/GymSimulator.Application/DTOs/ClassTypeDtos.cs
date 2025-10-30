namespace GymSimulator.Application.DTOs;

public record ClassTypeCreateDto(string Name, string? Description, bool IsActive = true);

public record ClassTypeUpdateDto(string Name, string? Description, bool IsActive);


