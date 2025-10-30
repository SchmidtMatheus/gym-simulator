using System;

namespace GymSimulator.Application.DTOs;

public class ClassDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ClassTypeName { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int Capacity { get; set; }
    public int CurrentBookings { get; set; }
    public string Instructor { get; set; } = string.Empty;

}
public record ClassCreateDto(Guid ClassTypeId, DateTime ScheduledAt, int DurationMinutes, int MaxCapacity, bool IsActive = true);

public record ClassUpdateDto(Guid ClassTypeId, DateTime ScheduledAt, int DurationMinutes, int MaxCapacity, bool IsActive, bool IsCancelled);


