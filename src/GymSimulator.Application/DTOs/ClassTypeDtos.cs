namespace GymSimulator.Application.DTOs;

public class ClassTypeFrequencyDto
{
    public Guid ClassTypeId { get; set; }
    public string ClassTypeName { get; set; } = string.Empty;
    public int BookingCount { get; set; }
    public double Percentage { get; set; }
}

public record ClassTypeCreateDto(string Name, string? Description, bool IsActive = true);

public record ClassTypeUpdateDto(string Name, string? Description, bool IsActive);


