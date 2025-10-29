using System;

namespace GymSimulator.Application.DTOs;

public record ClassCreateDto(int ClassTypeId, DateTime ScheduledAt, int DurationMinutes, int MaxCapacity, bool IsActive = true);

public record ClassUpdateDto(int ClassTypeId, DateTime ScheduledAt, int DurationMinutes, int MaxCapacity, bool IsActive, bool IsCancelled);


