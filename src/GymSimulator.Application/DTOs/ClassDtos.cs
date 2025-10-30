using System;

namespace GymSimulator.Application.DTOs;

public record ClassCreateDto(Guid ClassTypeId, DateTime ScheduledAt, int DurationMinutes, int MaxCapacity, bool IsActive = true);

public record ClassUpdateDto(Guid ClassTypeId, DateTime ScheduledAt, int DurationMinutes, int MaxCapacity, bool IsActive, bool IsCancelled);


