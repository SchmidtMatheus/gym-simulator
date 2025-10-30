using GymSimulator.Application.DTOs;

namespace GymSimulator.Application.Mappings
{
    public static class StudentMapping
    {
        public static StudentDto ToDto(this Domain.Entities.Student student)
        {
            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Phone = student.Phone,
                PlanTypeName = student.PlanType?.Name ?? "N/A"
            };
        }

        public static List<StudentDto> ToDto(this IEnumerable<Domain.Entities.Student> students)
        {
            return students.Select(s => s.ToDto()).ToList();
        }
    }
}
