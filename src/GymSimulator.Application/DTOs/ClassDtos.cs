using System;

namespace GymSimulator.Application.DTOs;

public class ClassDto
{
    public Guid Id { get; set; }
    public string ClassTypeName { get; set; } = string.Empty;
    public DateTime ScheduledAt { get; set; }
    public int DurationMinutes { get; set; }
    public int MaxCapacity { get; set; }
    public int CurrentParticipants { get; set; }
    public bool IsActive { get; set; }
    public bool IsCancelled { get; set; }
    public DateTime CreatedAt { get; set; }
}

public record ClassCreateDto(Guid ClassTypeId, DateTime ScheduledAt, int DurationMinutes, int MaxCapacity, bool IsActive = true);

public record ClassUpdateDto(Guid ClassTypeId, DateTime ScheduledAt, int DurationMinutes, int MaxCapacity, bool IsActive, bool IsCancelled);


