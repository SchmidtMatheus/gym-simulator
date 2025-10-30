using GymSimulator.Application.DTOs;
using GymSimulator.Domain.Entities;

namespace GymSimulator.Application.Mappings
{
    public static class ClassMapping
    {
        public static ClassDto ToDto(this Class classEntity)
        {
            return new ClassDto
            {
                Id = classEntity.Id,
                ClassTypeName = classEntity.ClassType?.Name ?? "N/A",
                ScheduledAt = classEntity.ScheduledAt,
                DurationMinutes = classEntity.DurationMinutes,
                MaxCapacity = classEntity.MaxCapacity,
                CurrentParticipants = classEntity.CurrentParticipants,
                IsActive = classEntity.IsActive,
                IsCancelled = classEntity.IsCancelled,
                CreatedAt = classEntity.CreatedAt
            };
        }

        public static List<ClassDto> ToDto(this IEnumerable<Class> classes)
        {
            return classes.Select(s => s.ToDto()).ToList();
        }
    }
}
